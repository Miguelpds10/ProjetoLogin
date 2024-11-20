using Microsoft.AspNetCore.Mvc;
using ProjetoLogin.Libraries.Filtro;
using ProjetoLogin.Libraries.Login;
using ProjetoLogin.Models;
using ProjetoLogin.Repositorio.Contract;
using System.Diagnostics;

namespace ProjetoLogin.Controllers
{
    public class HomeController : Controller
    {
        private IClienteRepositorio _clienteRepositorio;
        private LoginCliente _loginCliente;
        public HomeController(IClienteRepositorio clienteRepositorio, LoginCliente loginCliente)
        {
            _clienteRepositorio = clienteRepositorio;
            _loginCliente = loginCliente;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login([FromForm] Cliente cliente)
        {
            Cliente clienteDB = _clienteRepositorio.Login(cliente.Email, cliente.Senha);
            if(clienteDB.Email != null && clienteDB.Senha != null)
            {
                _loginCliente.Login(clienteDB);
                return new RedirectResult(Url.Action(nameof(PainelCliente)));
            }
            else
            {
                //Erro na sessão
                ViewData["MSG_E"] = "Usuario não localizado, por favor verifique e-mail e senha digitado";
                return View();
            }
        }
        public IActionResult PainelCliente()
        {
            ViewBag.Nome = _loginCliente.GetCliente().Nome;
            ViewBag.CPF = _loginCliente.GetCliente().CPF;
            ViewBag.Email = _loginCliente.GetCliente().Email;
            return View();
        }
        [ClienteAutorizacao]
        public IActionResult LogoutCliente()
        {
            _loginCliente.Logout();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Cadastrar()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Cadastrar([FromForm] Cliente cliente)
        {
            var CPFExit = _clienteRepositorio.BuscaCpfCliente(cliente.CPF).CPF;
            var EmailExit = _clienteRepositorio.BuscaEmailCliente(cliente.Email).Email;

            if (!string.IsNullOrWhiteSpace(CPFExit))
            {
                //CPF Cadastrado
                ViewData["MSG_CPF"] = "CPF já cadastrado, por favor verifique os dados digitado";
                return View();

            }
            else if (!string.IsNullOrWhiteSpace(EmailExit))
            {
                //Email Cadastrado
                ViewData["MSG_Email"] = "E-mail já cadastrado, por favor verifique os dados digitado";
                return View();
            }
            else if (ModelState.IsValid)
            {

                _clienteRepositorio.Cadastrar(cliente);
                return RedirectToAction(nameof(Login));
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
