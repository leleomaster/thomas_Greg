using ThomasGreg.Domain.Entities;
using ThomasGreg.Domain.Models;

namespace ThomasGreg.Application.Interfaces
{
    public interface IClienteService : IBaseService<Cliente, ClienteViewModel>
    {
        bool EmialExiste(string email);
    }
}
