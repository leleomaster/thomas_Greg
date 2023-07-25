using ThomasGreg.Domain.Models;
using ThomasGreg.Web.Sevices.Interfaces;

namespace ThomasGreg.Web.Sevices.Implementation
{
    public class LogradouroServicecs : ServiceBase<LogradouroViewModel>, ILogradouroServicecs
    {
        private const string _basePath = "/v1/logradouro";
        public LogradouroServicecs(IConfiguration configuration) 
            : base(_basePath, configuration)
        {
        }
    }
}
