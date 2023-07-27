using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ThomasGreg.Application.Interfaces;
using ThomasGreg.Domain.Entities;
using ThomasGreg.Domain.Models;
using ThomasGreg.Infrastructure.Contexts;
using ThomasGreg.Infrastructure.Interfaces;

namespace ThomasGreg.Application.Implementations
{
    public class ClienteService : BaseService<Cliente, ClienteViewModel>, IClienteService
    {
        private readonly IRepositoryCliente _repositoryCliente;
        private readonly SqlServerContext _context;
        private readonly IMapper _mapper;

        public ClienteService(SqlServerContext context, IMapper mapper, IRepositoryCliente repositoryCliente)
            : base(repositoryCliente)
        {
            _repositoryCliente = repositoryCliente;
            _mapper = mapper;
            _context = context;
        }

        public bool EmialExiste(string email)
        {
            return _repositoryCliente.EmialExiste(email);
        }

        public override async Task<IEnumerable<ClienteViewModel>> ObterTodos()
        {
            var clienteViewModel = await _context.Clientes.Include(c => c.Logradouros).ToListAsync();
            var model = _mapper.Map<IEnumerable<ClienteViewModel>>(clienteViewModel);

            return model;
        }
    }
}
