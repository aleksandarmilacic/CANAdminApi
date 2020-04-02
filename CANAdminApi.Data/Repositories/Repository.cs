using CANAdminApi.Data.Entities;
using CANAdminApi.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CANAdminApi.Data.Repositories
{
    /// <summary>
    /// Generic repository
    /// </summary>
    public class Repository<TEntity> : IGenericRepository<TEntity>
        where TEntity : EntityBase
    {
        public Context Context { get; }

        public Repository(Context context)
        {
            Context = context;
        }

        public virtual IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null)
        {
            var query = Context.Set<TEntity>().AsQueryable();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return query;
        }

        public virtual TEntity GetEntity(Expression<Func<TEntity, bool>> filter = null)
        {
            var query = Context.Set<TEntity>().AsQueryable();
            TEntity result = null;
            if (filter != null)
            {
                result = query.FirstOrDefault(filter);
            }
            return result;
        }

        public virtual IQueryable<TEntity> GetAllDeleted()
        {
            return Context.Set<TEntity>().Where(x => x.HasBeenDeleted != null);
        }

        public void Delete(TEntity item)
        {
            Context.Set<TEntity>().Remove(item);
        }

        public TEntity Add(TEntity item)
        {
            Context.Set<TEntity>().Add(item);
            return item;
        }

        public void AddMany(IEnumerable<TEntity> entities)
        {
            Context.Set<TEntity>().AddRange(entities);
        }

        public async Task<int> SaveAsync()
        {
            return await Context.SaveChangesAsync();
        }
    }
}
