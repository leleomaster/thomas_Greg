using AutoMapper;
using IdentityModel;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
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

        public virtual async Task<TModel> ObterPorId(int id)
        {
            var entity = await _context.Set<TEntity>().FindAsync(id);
            var model = _mapper.Map<TModel>(entity);

            return model;
        }

        public virtual async Task<IEnumerable<TModel>> ObterTodos(Expression<Func<TEntity, bool>> filter = null, Expression<Func<TEntity, bool>> includes = null)
        {
            List<TEntity> entity = null;

            if (filter != null && includes != null)
            {
                entity = await _context.Set<TEntity>().Include(includes).Where(filter).ToListAsync();
            }
            else if (filter != null)
            {
                entity = await _context.Set<TEntity>().Where(filter).ToListAsync();
            }
            else if (includes != null)
            {
                entity = await _context.Set<TEntity>().Include(includes).ToListAsync();
            }
            else
            {
                entity = await _context.Set<TEntity>().ToListAsync();
            }
            var model = _mapper.Map<IEnumerable<TModel>>(entity);
            return model;
        }

        public virtual async Task Adicionar(TModel model)
        {
            var entity = _mapper.Map<TEntity>(model);

            _context.Set<TEntity>().Add(entity);
            await _context.SaveChangesAsync();
        }

        public virtual async Task Atualizar(TModel model)
        {
            var entity = _mapper.Map<TEntity>(model);

            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public virtual async Task Deletar(int id)
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
