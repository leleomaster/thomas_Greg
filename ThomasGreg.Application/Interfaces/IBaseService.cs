using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThomasGreg.Application.Interfaces
{
    public interface IBaseService<TEntity, TModel>
        where TEntity : class
        where TModel : class
    {
        Task Adicionar(TModel model);


        Task<TModel> ObterPorId(int id);

        Task<IEnumerable<TModel>> ObterTodos();

        Task Atualizar(TModel model);

        Task Deletar(int id);

        void Dispose();
    }
}
