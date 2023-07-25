using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ThomasGreg.Domain.Models;
using ThomasGreg.Infrastructure;

namespace ThomasGreg.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;

        public AuthenticateController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<UserTokenViewModel>> Authenticate([FromBody] UserInfoViewModel model)
        {
            if (await ValidateCredentials(model))
            {
                return BuildToken(model);
            }
            else
            {
                return new UnauthorizedResult();
            }
        }

        private async Task<bool> ValidateCredentials(UserInfoViewModel user)
        {
            bool credenciaisValidas = false;
            if (user is not null && !String.IsNullOrWhiteSpace(user.UserID))
            {
                // Verifica a existência do usuário nas tabelas do
                // ASP.NET Core Identity
                var userIdentity = await _userManager.FindByNameAsync(user.UserID);
                if (userIdentity is not null)
                {
                    // Efetua o login com base no Id do usuário e sua senha
                    var resultadoLogin = await _signInManager.CheckPasswordSignInAsync(userIdentity, user.Password, false);
                    credenciaisValidas = resultadoLogin.Succeeded;
                }
            }

            return credenciaisValidas;
        }


        private UserTokenViewModel BuildToken(UserInfoViewModel user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:key"]);
            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Issuer"];

            List<Claim> claims = new List<Claim>() {
                new Claim(ClaimTypes.Name, user.UserID.ToString())
                //,new Claim(ClaimTypes.NameIdentifier, modelLogin.Email)
                //,new Claim("OtherProperties","Example Role")
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = issuer,
                Audience = audience,
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(20),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var expiration = DateTime.UtcNow.AddHours(1);

            return new UserTokenViewModel()
            {
                Token = tokenHandler.WriteToken(token),
                //Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration
            };
        }
    }
}
