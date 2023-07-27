using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ThomasGreg.Application.Interfaces;
using ThomasGreg.Infrastructure.Interfaces;

namespace ThomasGreg.Application.Implementations
{
    public abstract class BaseService<TEntity, TModel> : IDisposable, IBaseService<TEntity, TModel>
             where TEntity : class
        where TModel : class
    {
        private readonly IRepositoryBase<TEntity, TModel> _repositoryBase;
        public BaseService(IRepositoryBase<TEntity, TModel> repositoryBase)
        {
            _repositoryBase = repositoryBase;
        }
        public virtual async Task Adicionar(TModel model)
        {
            await _repositoryBase.Adicionar(model);
        }

        public virtual async Task Atualizar(TModel model)
        {
            await _repositoryBase.Atualizar(model);
        }

        public virtual async Task Deletar(int id)
        {
            await _repositoryBase.Deletar(id);
        }

        public virtual void Dispose()
        {
            _repositoryBase.Dispose();
        }

        public virtual async Task<TModel> ObterPorId(int id)
        {
            return await _repositoryBase.ObterPorId(id);
        }


        public virtual async Task<IEnumerable<TModel>> ObterTodos()
        {
            return await _repositoryBase.ObterTodos();
        }
    }
}
