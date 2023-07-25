using System;
using System.Collections.Generic;
using System.Linq;
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
        public async Task Adicionar(TModel model)
        {
            try
            {
                await _repositoryBase.Adicionar(model);
            }
            catch (Exception ex)
            {
                // log
            }
        }

        public async Task Atualizar(TModel model)
        {
            try
            {

                await _repositoryBase.Atualizar(model);
            }
            catch (Exception ex)
            {
                // log
            }
        }

        public async Task Deletar(int id)
        {
            try
            {
                await _repositoryBase.Deletar(id);
            }
            catch (Exception ex)
            {
                // log
            }
        }

        public void Dispose()
        {
            _repositoryBase.Dispose();
        }

        public async Task<TModel> ObterPorId(int id)
        {
            try
            {
                return await _repositoryBase.ObterPorId(id);
            }
            catch (Exception ex)
            {
                // log
                return null;
            }
        }


        public async Task<IEnumerable<TModel>> ObterTodos()
        {
            try
            {
                return await _repositoryBase.ObterTodos();
            }
            catch (Exception ex)
            {
                // log
                return Enumerable.Empty<TModel>();
            }
        }
    }
}
