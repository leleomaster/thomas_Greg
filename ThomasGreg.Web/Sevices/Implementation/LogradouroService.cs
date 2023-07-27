using ThomasGreg.Domain.Models;
using ThomasGreg.Web.Sevices.Interfaces;

namespace ThomasGreg.Web.Sevices.Implementation
{
    public class LogradouroService : ServiceBase<LogradouroViewModel>, ILogradouroService
    {
        private const string _basePath = "/api/v1/logradouro";
        public LogradouroService(IConfiguration configuration) 
            : base(_basePath, configuration)
        {
        }
    }
}
