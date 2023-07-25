using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ThomasGreg.Domain.Models;
using ThomasGreg.Web.Sevices.Interfaces;

namespace ThomasGreg.Web.Controllers
{
   // [Authorize]
    public class LogradouroController : Controller
    {
        private readonly ILogradouroServicecs _serviceBase;
        public LogradouroController(ILogradouroServicecs serviceBase)
        {
            _serviceBase = serviceBase;
        }

        private async Task<string> GetToken()
        {
            var token = await HttpContext.GetTokenAsync(JwtBearerDefaults.AuthenticationScheme, "access_token");
            var token15 = await HttpContext.GetTokenAsync(JwtBearerDefaults.AuthenticationScheme, "teste_access_token");

            var token1 = await HttpContext.GetTokenAsync("teste_access_token");
            var token2 = await HttpContext.GetTokenAsync("access_token");
            return token;
        }
        public async Task<IActionResult> Index()
        {
            var result = await _serviceBase.ObterTodos(await GetToken());

            return View(result);
        }

        // GET: Logradouro/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }
            var logradouro = _serviceBase.ObterPorId(id, await GetToken());

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
                await _serviceBase.Adicionar(logradouroViewModel, await GetToken());

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
            var logradouro = await _serviceBase.ObterPorId(id, await GetToken());

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
                    await _serviceBase.Atualizar(logradouroViewModel, await GetToken());
                }
                catch (DbUpdateConcurrencyException)
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

            var model = await _serviceBase.ObterPorId(id, await GetToken());
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

            var model = await _serviceBase.ObterPorId(id, await GetToken());
            if (model == null)
            {
                return NotFound();
            }

            await _serviceBase.Remover(id, await GetToken());

            return RedirectToAction(nameof(Index));
        }
    }
}
