namespace ThomasGreg.Web2.Models
{
    public class SiteKeys
    {
        private static IConfigurationSection _configuration;
        public static void Configure(IConfigurationSection configuration)
        {
            _configuration = configuration;
        }

        public static string WebSiteDomain => _configuration["Issuer"];
        public static string Token => _configuration["Key"];

    }
}
