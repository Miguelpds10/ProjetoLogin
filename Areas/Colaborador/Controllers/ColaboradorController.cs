using ProjetoLogin;
using Microsoft.AspNetCore.Mvc;
using ProjetoLogin.Libraries.Filtro;
using ProjetoLogin.Models.Constants;
using ProjetoLogin.Repositorio.Contract;
using ProjetoLogin.Models;
using ProjetoLogin.Libraries.Middleware;

namespace ProjetoLogin.Areas.Colaborador.Controllers
{
    [Area("Colaborador")]
    [ColaboradorAutorizacao(ColaboradorTipoConstant.Gerente)]
    public class ColaboradorController : Controller
    {
        private IColaboradorRepositorio _colaboradorRepositorio;
        public ColaboradorController(IColaboradorRepositorio colaboradorRepositorio)
        {
            _colaboradorRepositorio = colaboradorRepositorio;
        }
        public IActionResult Index()
        {
            return View(_colaboradorRepositorio.ObterTodosColaboradores());
        }
        [HttpGet]
        public IActionResult Cadastrar()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Cadastrar(Models.Colaborador colaborador)
        {
            colaborador.Tipo = ColaboradorTipoConstant.Comun;
            _colaboradorRepositorio.Cadastrar(colaborador);
            TempData["MGS_S"] = "Registro salvo com sucesso!";
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        [ValidateHttpReferer]
        public IActionResult Atualizar(int id)
        {
            Models.Colaborador colaborador = _colaboradorRepositorio.ObterColaborador(id);
            return View(colaborador);
        }
        [HttpPost]
        public IActionResult Atualizar(Models.Colaborador colaborador)
        {
            if (ModelState.IsValid)
            {
                _colaboradorRepositorio.Atualizar(colaborador);
                TempData["MGS_S"] = "Registro salvo com sucesso!";
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
    }
}
