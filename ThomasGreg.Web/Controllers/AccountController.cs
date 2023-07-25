using Microsoft.AspNetCore.Mvc;
using NuGet.Common;
using System.Net.Http.Headers;
using System.Net.Http;
using ThomasGreg.Domain.Models;
using ThomasGreg.Web.Sevices;
using ThomasGreg.Web.Utils;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace ThomasGreg.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly HttpClient _httpClient = HttpClientSingleton.GetHttpClient();
        private string endpoint = "/api/Authenticate";
        private string basePath = "";
        private readonly IConfiguration _configuration;

        public AccountController(IConfiguration configuration)
        {
            _configuration = configuration;
            basePath = _configuration["thomasGregAPI:baseURL"] + endpoint;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserInfoViewModel model)
        {
            var response = await _httpClient.PostAsJson($"{basePath}/login", model);

            if (response.IsSuccessStatusCode)
            {
                var userToken = await response.RedContentAsync<UserTokenViewModel>();

                // HttpContext.Session.SetString("JWToken", userToken.Token);
                // 

                //    List<Claim> claims = new List<Claim>() {
                //    new Claim(ClaimTypes.NameIdentifier, modelLogin.Email),
                //    new Claim("OtherProperties","Example Role")

                //};

                ClaimsIdentity claimsIdentity = new ClaimsIdentity(
                    CookieAuthenticationDefaults.AuthenticationScheme);

                AuthenticationProperties properties = new AuthenticationProperties()
                {
                    AllowRefresh = true,
                    IsPersistent = model.ManterLogado
                };

                properties.StoreTokens(
                    new List<AuthenticationToken>()
                    {
                            new AuthenticationToken()
                            {
                                Name = "teste_access_token", Value =userToken.Token
                            }
                    }
                    );

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity), properties);

                return RedirectToAction("Index", "Home");
            }
            else
                return View(model);
        }

        public IActionResult Logout()
        {
            //HttpContext.Request.
            return RedirectToAction("index", "Home");
        }
    }
}
