using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ThomasGreg.Domain.Models;

namespace ThomasGreg.Web.Controllers
{
    [Authorize]
    public class ClienteController : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View();
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ClienteViewModel model)
        {
            return View();
        }
    }
}
