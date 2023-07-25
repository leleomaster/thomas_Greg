using ThomasGreg.Application.Interfaces;
using ThomasGreg.Domain.Entities;
using ThomasGreg.Domain.Models;
using ThomasGreg.Infrastructure.Interfaces;

namespace ThomasGreg.Application.Implementations
{
    public class ClienteService : BaseService<Cliente, ClienteViewModel>, IClienteService
    {
        private readonly IRepositoryCliente _repositoryCliente;
        public ClienteService(IRepositoryCliente repositoryCliente)
            : base(repositoryCliente)
        {
            _repositoryCliente = repositoryCliente;
        }

        public bool EmialExiste(string email)
        {
            return _repositoryCliente.EmialExiste(email);
        }
    }
}
