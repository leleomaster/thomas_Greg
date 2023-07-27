using ThomasGreg.Domain.Models;
using ThomasGreg.Web.Sevices.Interfaces;

namespace ThomasGreg.Web.Sevices.Implementation
{
    public class ClienteService : ServiceBase<ClienteViewModel>, IClienteService
    {
        private const string _basePath = "/api/v1/cliente";
        public ClienteService(IConfiguration configuration)
            : base(_basePath, configuration)
        {
        }
    }
}
