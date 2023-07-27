using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThomasGreg.Domain.Entities;
using ThomasGreg.Domain.Models;
using ThomasGreg.Infrastructure.Contexts;
using ThomasGreg.Infrastructure.Interfaces;

namespace ThomasGreg.Infrastructure.Implementations
{
    public class RepositoryLogradouro : RepositoryBase<Logradouro, LogradouroViewModel>, IRepositoryLogradouro
    {
        private readonly SqlServerContext _context;
        private readonly IMapper _mapper;

        public RepositoryLogradouro(SqlServerContext Context, IMapper mapper)
            : base(Context, mapper)
        {
            _context = Context;
            _mapper = mapper;
        }


    }
}
