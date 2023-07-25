namespace ThomasGreg.Web.Sevices.Interfaces
{
    public interface IServiceBase<T> where T : class
    {
        Task<IEnumerable<T>> ObterTodos(string token);
        Task<T> ObterPorId(long id, string token);
        Task<T> Adicionar(T model, string token);
        Task<T> Atualizar(T model, string token);
        Task<bool> Remover(long id, string token);
    }
}
