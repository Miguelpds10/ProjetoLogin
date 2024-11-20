using Microsoft.AspNetCore.Mvc;
using ProjetoLogin.Libraries.Filtro;
using ProjetoLogin.Libraries.Login;
using ProjetoLogin.Models;
using ProjetoLogin.Models.Constants;
using ProjetoLogin.Repositorio.Contract;

namespace ProjetoLogin.Areas.Colaborador.Controllers

{
    [Area("Colaborador")]
    public class HomeController : Controller
    {
        private IColaboradorRepositorio _repositorioColaborador;
        private LoginColaborador _loginColaborador;
        private IClienteRepositorio _clienteRepositorio;

        public HomeController(IColaboradorRepositorio repositorioColaborador, LoginColaborador loginColaborador)
        {
            _repositorioColaborador = repositorioColaborador;
            _loginColaborador = loginColaborador;
        }
        [ColaboradorAutorizacao]
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login([FromForm] Models.Colaborador colaborador)
        {
            Models.Colaborador colaboradorDB = _repositorioColaborador.Login(colaborador.Email, colaborador.Senha);

            if (colaboradorDB.Email != null && colaboradorDB.Senha != null)
            {
                _loginColaborador.Login(colaboradorDB);

                return new RedirectResult(Url.Action(nameof(Painel)));
            }
            else
            {
                ViewData["MSG_E"] = "Usuário não encontrado, verfique o e-mail e senha digitado!";
                return View();
            }
        }

        /*public IActionResult PainelGerente()
        {
            ViewBag.Nome = _loginColaborador.GetColaborador().Nome;
            ViewBag.Tipo = _loginColaborador.GetColaborador().Tipo;
            ViewBag.Email = _loginColaborador.GetColaborador().Email;
            return View();
        }
        public IActionResult PainelComum()
        {
            ViewBag.Nome = _loginColaborador.GetColaborador().Nome;
            ViewBag.Tipo = _loginColaborador.GetColaborador().Tipo;
            ViewBag.Email = _loginColaborador.GetColaborador().Email;
            return View();
        }*/

        [ColaboradorAutorizacao]
        public IActionResult Painel()
        {
            return View();
        }
        [ColaboradorAutorizacao]
        public IActionResult Logout()
        {
            _loginColaborador.Logout();
            return RedirectToAction("Login", "Home");
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
    }
}
