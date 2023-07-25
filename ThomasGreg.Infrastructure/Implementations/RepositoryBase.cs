using AutoMapper;
using IdentityModel;
using Microsoft.EntityFrameworkCore;
using ThomasGreg.Infrastructure.Contexts;
using ThomasGreg.Infrastructure.Interfaces;

namespace ThomasGreg.Infrastructure.Implementations
{
    public abstract class RepositoryBase<TEntity, TModel> : IDisposable, IRepositoryBase<TEntity, TModel> 
        where TEntity : class
        where TModel : class
    {
        private readonly SqlServerContext _context;
        private readonly IMapper _mapper;

        public RepositoryBase(SqlServerContext Context, IMapper mapper)
        {
            _context = Context;
            _mapper = mapper;
        }

        public async Task<TModel> ObterPorId(int id)
        {
            var entity = await _context.Set<TEntity>().FindAsync(id); 
            var model = _mapper.Map<TModel>(entity);

            return model;
        }

        public async Task<IEnumerable<TModel>> ObterTodos()
        {
            var entity = await _context.Set<TEntity>().ToListAsync();
            var model = _mapper.Map<IEnumerable<TModel>>(entity);
            return model;
        }

        public async Task Adicionar(TModel model)
        {
            var entity = _mapper.Map<TEntity>(model);

            _context.Set<TEntity>().Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Atualizar(TModel model)
        {
            var entity = _mapper.Map<TEntity>(model);

            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task Deletar(int id)
        {
            var entity = await _context.Set<TEntity>().FindAsync(id);

            _context.Set<TEntity>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public virtual void Dispose()
        {
            _context.Dispose();
        }
    }
}
