using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ThomasGreg.Domain.Entities;
using ThomasGreg.Domain.Models;
using ThomasGreg.Web.Sevices.Interfaces;
using ThomasGreg.Web.Utils;

namespace ThomasGreg.Web.Controllers
{
    [Authorize]
    public class ClienteController : Controller
    {
        private readonly IClienteService _serviceBase;
        private readonly ILogradouroService _logradouroService;
        public ClienteController(IClienteService serviceBase, ILogradouroService logradouroService)
        {
            _serviceBase = serviceBase;
            _logradouroService = logradouroService;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _serviceBase.ObterTodos(Helpers.GetTokenSession(HttpContext));
            Helpers.ConvertImgDataURL(result);

            return View(result);
        }

        // GET: Cliente/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }
            var Cliente = await _serviceBase.ObterPorId(id, Helpers.GetTokenSession(HttpContext));
            Helpers.ConvertImgDataURL(Cliente);

            if (Cliente == null)
            {
                return NotFound();
            }
            await SetViewBagLogradouros();
            return View(Cliente);
        }

        // GET: Cliente/Create
        public async Task<IActionResult> Create()
        {
            await SetViewBagLogradouros();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ClienteViewModel ClienteViewModel)
        {
            if (ModelState.IsValid)
            {
                var file = HttpContext.Request.Form.Files["postedFile"];

                if (file != null && file.Length > 0)
                {
                    using (var target = new MemoryStream())
                    {
                        file.CopyTo(target);
                        ClienteViewModel.Logotipo = target.ToArray();
                    }

                    // ClienteViewModel.Logotipo = Helpers.GetByteArrayFromImage(file);

                    await _serviceBase.Adicionar(ClienteViewModel, Helpers.GetTokenSession(HttpContext));

                    return RedirectToAction(nameof(Index));
                }
            }
            await SetViewBagLogradouros();
            return View(ClienteViewModel);
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }
            var Cliente = await _serviceBase.ObterPorId(id, Helpers.GetTokenSession(HttpContext));
            Helpers.ConvertImgDataURL(Cliente);

            if (Cliente == null)
            {
                return NotFound();
            }
            await SetViewBagLogradouros();
            return View(Cliente);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ClienteViewModel ClienteViewModel)
        {
            if (id != ClienteViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var file = HttpContext.Request.Form.Files["postedFile"];

                    if (file != null && file.Length > 0)
                    {
                        using (var target = new MemoryStream())
                        {
                            file.CopyTo(target);
                            ClienteViewModel.Logotipo = target.ToArray();
                        }
                        await _serviceBase.Atualizar(ClienteViewModel, Helpers.GetTokenSession(HttpContext));
                    }
                }
                catch (Exception ex)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            await SetViewBagLogradouros();
            return View(ClienteViewModel);
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
            Helpers.ConvertImgDataURL(model);
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

        private async Task SetViewBagLogradouros()
        {
            var logradouros = await _logradouroService.ObterTodos(Helpers.GetTokenSession(HttpContext));

            ViewBag.Logradouros = Helpers.ConvertLogradouroParaSelectListItem(logradouros);
        }
    }
}
