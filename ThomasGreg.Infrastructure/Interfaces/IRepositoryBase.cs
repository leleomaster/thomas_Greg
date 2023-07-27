using System.Linq.Expressions;

namespace ThomasGreg.Infrastructure.Interfaces
{
    public interface IRepositoryBase<TEntity, TModel>
        where TEntity : class
        where TModel : class
    {
        Task Adicionar(TModel model);


        Task<TModel> ObterPorId(int id);

        Task<IEnumerable<TModel>> ObterTodos(Expression<Func<TEntity, bool>> filter = null, Expression<Func<TEntity, bool>> includes = null);

        Task Atualizar(TModel model);

        Task Deletar(int id);

        void Dispose();
    }
}
