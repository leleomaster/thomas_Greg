using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ThomasGreg.Domain.Models;
using ThomasGreg.Web.Sevices;
using ThomasGreg.Web.Utils;

namespace ThomasGreg.Web2.Controllers
{
    public class AccountController : BaseController
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
            try
            {
                var response = await _httpClient.PostAsJson($"{basePath}/login", model);

                if (response.IsSuccessStatusCode)
                {
                    var userToken = await response.RedContentAsync<UserTokenViewModel>();

                    HttpContext.Session.SetString("JWToken", userToken.Token);

                    // Show Success Message -"Welcome!"    
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    //  Show Error Message- "Invalid Credentials."    
                    return View("Login", model);
                }
            }
            catch (Exception ex)
            {
                //Show Error Message- ex.Message    
                return View("Login", model);
            }
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
