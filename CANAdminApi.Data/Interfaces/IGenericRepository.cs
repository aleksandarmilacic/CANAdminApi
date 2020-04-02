using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CANAdminApi.Data.Interfaces
{
    /// <summary>
    /// Represents repository of items, with functionality to get entities, add new entities and delete existing.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Gets all entities.
        /// </summary>
        /// <returns>Query of entities.</returns>
        IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null);

        /// <summary>
        /// Inserts the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>Inserted entity.</returns>
        TEntity Add(TEntity entity);

        /// <summary>
        /// Inserts the list of specified entities.
        /// </summary>
        /// <param name="entities">The entity list.</param>
        void AddMany(IEnumerable<TEntity> entities);

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Delete(TEntity entity);

        /// <summary>
        /// Saves context cahnges async
        /// </summary>
        /// <returns>Async result</returns>
        Task<int> SaveAsync();
    }
}
