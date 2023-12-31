﻿namespace ThomasGreg.Web.Sevices.Interfaces
{
    public interface IServiceBase<T> where T : class
    {
        Task<IEnumerable<T>> ObterTodos(string token);
        Task<T> ObterPorId(long id, string token);
        Task<bool> Adicionar(T model, string token);
        Task<bool> Atualizar(T model, string token);
        Task<bool> Remover(long id, string token);
    }
}
