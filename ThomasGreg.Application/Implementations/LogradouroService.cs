using AutoMapper;
using IdentityModel;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using ThomasGreg.Application.Interfaces;
using ThomasGreg.Domain.Entities;
using ThomasGreg.Domain.Models;
using ThomasGreg.Infrastructure.Contexts;
using ThomasGreg.Infrastructure.Implementations;
using ThomasGreg.Infrastructure.Interfaces;

namespace ThomasGreg.Application.Implementations
{
    public class LogradouroService : BaseService<Logradouro, LogradouroViewModel>, ILogradouroService
    {
        private readonly IRepositoryLogradouro _repositoryLogradouro;
        private readonly SqlServerContext _context;
        private readonly IMapper _mapper;   

        public LogradouroService(SqlServerContext context, IMapper mapper,IRepositoryLogradouro repositoryLogradouro)
            : base(repositoryLogradouro)
        {
            _repositoryLogradouro = repositoryLogradouro;
            _context = context;
            _mapper = mapper;
        }

        public override async Task<IEnumerable<LogradouroViewModel>> ObterTodos()
        {
            var logradouroViewModel = await _context.Logradouros.Include(c => c.Clientes).ToListAsync();
            var model = _mapper.Map<IEnumerable<LogradouroViewModel>>(logradouroViewModel);

            return model;
        }
    }
}
