using ThomasGreg.Domain.Entities;
using ThomasGreg.Domain.Models;
using ThomasGreg.Infrastructure.Implementations;

namespace ThomasGreg.Infrastructure.Interfaces
{
    public interface IRepositoryCliente : IRepositoryBase<Cliente, ClienteViewModel>
    {
        bool EmialExiste(string email);
    }
}
