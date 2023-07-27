using AutoMapper;
using ThomasGreg.Domain.Entities;
using ThomasGreg.Domain.Models;
using ThomasGreg.Infrastructure.Contexts;
using ThomasGreg.Infrastructure.Interfaces;
using System.Linq;
using System.Linq.Expressions;
using System.Linq;
using Microsoft.EntityFrameworkCore;

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

        public override async Task Adicionar(ClienteViewModel model)
        {
            var entity = _mapper.Map<Cliente>(model);
            var idsLogradouros = model.LogradouroId;

            try
            {
                entity.Logradouros = _context.Logradouros.Where(l => idsLogradouros.Contains(l.Id)).ToList();

                _context.Set<Cliente>().Add(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool EmialExiste(string email)
        {
            var cliente = _context.Clientes.FirstOrDefault(c => c.Email == email);

            return cliente != null;
        }
    }
}
