using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace IMDBAssignment.Repositories.Interfaces
{
    /// <summary>
    /// Repository interface.
    /// </summary>
    public interface IRepository<T> : IRepository
    {
        /// <summary>
        /// Add an entity.
        /// </summary>
        /// <param name="entity">The entity to be added.</param>
        /// <returns>The added entity.</returns>
        T Add(T entity);

        /// <summary>
        /// Update an entity.
        /// </summary>
        /// <param name="entity">The entity to be updated.</param>
        /// <returns>The updated entity.</returns>
        T Update(T entity);

        /// <summary>
        /// Delete an entity.
        /// </summary>
        /// <param name="entity">The entity to be deleted.</param>
        void Delete(T entity);

        /// <summary>
        /// Get a list of entities.
        /// </summary>
        /// <returns>The list of entities.</returns>
        IEnumerable<T> List();

        /// <summary>
        /// Begin a transaction.
        /// </summary>
        /// <returns>The transaction.</returns>
        IDbContextTransaction BeginTransaction();

        /// <summary>
        /// Search for entities.
        /// </summary>
        /// <param name="expression">The search expresion.</param>
        /// <returns>The set of matching entities.</returns>
        IQueryable<T> Search(Expression<Func<T, bool>> expression);

    }
}
