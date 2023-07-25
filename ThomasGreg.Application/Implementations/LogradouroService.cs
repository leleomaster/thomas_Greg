using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThomasGreg.Application.Interfaces;
using ThomasGreg.Domain.Entities;
using ThomasGreg.Domain.Models;
using ThomasGreg.Infrastructure.Interfaces;

namespace ThomasGreg.Application.Implementations
{
    public class LogradouroService : BaseService<Logradouro, LogradouroViewModel>, ILogradouroService
    {
        private readonly IRepositoryLogradouro _repositoryLogradouro;
        public LogradouroService(IRepositoryLogradouro repositoryLogradouro)
            : base(repositoryLogradouro)
        {
            _repositoryLogradouro = repositoryLogradouro;
        }
    }
}
