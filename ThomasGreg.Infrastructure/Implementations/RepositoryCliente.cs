using AutoMapper;
using ThomasGreg.Domain.Entities;
using ThomasGreg.Domain.Models;
using ThomasGreg.Infrastructure.Contexts;
using ThomasGreg.Infrastructure.Interfaces;

namespace ThomasGreg.Infrastructure.Implementations
{
    public class RepositoryCliente : RepositoryBase<Cliente, ClienteViewModel>, IRepositoryCliente
    {
        private readonly SqlServerContext _context;
        private readonly IMapper _mapper;
        public RepositoryCliente(SqlServerContext Context, IMapper mapper)
            : base(Context, mapper)
        {
            _context = Context;
            _mapper = mapper;
        }

        public bool EmialExiste(string email)
        {
           var cliente = _context.Clientes.FirstOrDefault(c => c.Email == email);

            return cliente != null;
        }
    }
}
