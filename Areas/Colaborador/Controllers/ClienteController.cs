using Microsoft.AspNetCore.Mvc;
using ProjetoLogin.Libraries.Middleware;
using ProjetoLogin.Models;
using ProjetoLogin.Models.Constants;
using ProjetoLogin.Repositorio.Contract;

namespace ProjetoLogin.Areas.Colaborador.Controllers
{
    [Area("Colaborador")]
    public class ClienteController : Controller
    {
        private IClienteRepositorio _clienteRepositorio;

        public ClienteController(IClienteRepositorio clienteRepositorio)
        {
            _clienteRepositorio = clienteRepositorio;
        }
        public IActionResult Index()
        {
            return View(_clienteRepositorio.ObterTodosClientes());
        }
        public IActionResult Cadastrar()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Cadastrar([FromForm] Cliente cliente)
        {
            cliente.Situacao = SituacaoConstant.Ativo;

            _clienteRepositorio.Cadastrar(cliente);
            return RedirectToAction(nameof(Cadastrar));
        }
        [ValidateHttpReferer]
        public IActionResult Ativar(int Id)
        {
            _clienteRepositorio.Ativar(Id);
            return RedirectToAction(nameof(Index));
        }
        [ValidateHttpReferer]
        public IActionResult Desativar(int Id)
        {
            _clienteRepositorio.Desativar(Id);
            return RedirectToAction(nameof(Index));
        }
    }
}
