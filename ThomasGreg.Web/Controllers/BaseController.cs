using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ThomasGreg.Domain.Models;
using ThomasGreg.Web2.Models;

namespace ThomasGreg.Web2.Controllers
{
    public class BaseController : Controller
    {
        public async Task SetSessionAuthenticationTicket(string token)
        {
            await Task.Run(action: () =>
            {
                HttpContext.Session.SetString("JWToken", token);
            });
        }
    }
}
