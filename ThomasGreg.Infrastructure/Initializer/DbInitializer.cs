using IdentityModel;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace ThomasGreg.Infrastructure.Initializer
{
    public  class DbInitializer : IDbInitializer
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbInitializer(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public void Initialize()
        {
            var adminis = "Admin";
            var cliente = "Client";

            if (_roleManager.FindByNameAsync(adminis).Result != null) return;

            _roleManager.CreateAsync(new IdentityRole(adminis)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(cliente)).GetAwaiter().GetResult();

            ApplicationUser admin = new ApplicationUser()
            {
                UserName = "leonardo-admin",
                Email = "leonardo-admin@gmail.com",
                EmailConfirmed = true,
                PhoneNumber = "+55 (11) 12345-6789",
                PrimeiroNome = "leonardo",
                UltimoNome = "Admin"
            };

            _userManager.CreateAsync(admin, "@Lc123456").GetAwaiter().GetResult();
            _userManager.AddToRoleAsync(admin, adminis).GetAwaiter().GetResult();

            var adminClaims = _userManager.AddClaimsAsync(admin, new Claim[]
            {
                new Claim(JwtClaimTypes.Name, $"{admin.PrimeiroNome} {admin.UltimoNome}"),
                new Claim(JwtClaimTypes.GivenName, admin.PrimeiroNome),
                new Claim(JwtClaimTypes.FamilyName,  admin.UltimoNome ),
                new Claim(JwtClaimTypes.Role, adminis)

            }).Result;

            ApplicationUser client = new ApplicationUser()
            {
                UserName = "leonardo-client",
                Email = "leonardo-client@gmail.com",
                EmailConfirmed = true,
                PhoneNumber = "+55 (34) 12345-6789",
                PrimeiroNome = "leonardo",
                UltimoNome = "client"
            };

            _userManager.CreateAsync(client, "@Lc123456").GetAwaiter().GetResult();
            _userManager.AddToRoleAsync(client, cliente).GetAwaiter().GetResult();

            var clientClaims = _userManager.AddClaimsAsync(client, new Claim[]
            {
                new Claim(JwtClaimTypes.Name, $"{client.PrimeiroNome} {client.UltimoNome}"),
                new Claim(JwtClaimTypes.GivenName, client.PrimeiroNome),
                new Claim(JwtClaimTypes.FamilyName,  client.UltimoNome ),
                new Claim(JwtClaimTypes.Role, cliente)

            }).Result;
        }
    }
}
