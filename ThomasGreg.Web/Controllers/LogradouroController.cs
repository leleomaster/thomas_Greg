using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ThomasGreg.Domain.Models;
using ThomasGreg.Web.Sevices.Interfaces;
using ThomasGreg.Web.Utils;

namespace ThomasGreg.Web.Controllers
{
    [Authorize]
    public class LogradouroController : Controller
    {
        private readonly ILogradouroService _serviceBase;
        public LogradouroController(ILogradouroService serviceBase)
        {
            _serviceBase = serviceBase;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _serviceBase.ObterTodos(Helpers.GetTokenSession(HttpContext));

            return View(result);
        }

        // GET: Logradouro/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }
            var logradouro = await _serviceBase.ObterPorId(id, Helpers.GetTokenSession(HttpContext));

            if (logradouro == null)
            {
                return NotFound();
            }

            return View(logradouro);
        }

        // GET: Logradouro/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nome,Numero")] LogradouroViewModel logradouroViewModel)
        {
            if (ModelState.IsValid)
            {
                await _serviceBase.Adicionar(logradouroViewModel, Helpers.GetTokenSession(HttpContext));

                return RedirectToAction(nameof(Index));
            }
            return View(logradouroViewModel);
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }
            var logradouro = await _serviceBase.ObterPorId(id, Helpers.GetTokenSession(HttpContext));

            if (logradouro == null)
            {
                return NotFound();
            }

            return View(logradouro);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Numero")] LogradouroViewModel logradouroViewModel)
        {
            if (id != logradouroViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _serviceBase.Atualizar(logradouroViewModel, Helpers.GetTokenSession(HttpContext));
                }
                catch (Exception ex)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(logradouroViewModel);
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            var model = await _serviceBase.ObterPorId(id, Helpers.GetTokenSession(HttpContext));
            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            var model = await _serviceBase.ObterPorId(id, Helpers.GetTokenSession(HttpContext));
            if (model == null)
            {
                return NotFound();
            }

            await _serviceBase.Remover(id, Helpers.GetTokenSession(HttpContext));

            return RedirectToAction(nameof(Index));
        }
    }
}
