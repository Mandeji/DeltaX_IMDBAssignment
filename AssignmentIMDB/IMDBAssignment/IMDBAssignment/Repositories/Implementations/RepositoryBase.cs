using IMDBAssignment.Repositories.Context;
using IMDBAssignment.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace IMDBAssignment.Repositories.Implementations
{
    /// <summary>
    /// Base class for repositories.
    /// </summary>
    public class RepositoryBase<T> : IRepository where T : class
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="repositoryContext">The repository context.</param>
        public RepositoryBase(RepositoryContext repositoryContext)
        {
            Context = repositoryContext;
        }

        /// <summary>
        /// Get the repository context.
        /// </summary>
        protected RepositoryContext Context { get; private set; }

        /// <summary>
        /// Add an entity.
        /// </summary>
        /// <param name="entity">The entity to be added.</param>
        /// <returns>The added entity.</returns>
        public T Add(T entity)
        {
            return Context.Set<T>().Add(entity).Entity;
        }

        /// <summary>
        /// Update an entity.
        /// </summary>
        /// <param name="entity">The entity to be updated.</param>
        /// <returns>The updated entity.</returns>
        public T Update(T entity)
        {
            Context.Attach(entity);
            Context.Entry(entity).State = EntityState.Modified;
            return entity;
        }

        /// <summary>
        /// Delete an entity.
        /// </summary>
        /// <param name="entity">The entity to be deleted.</param>
        public void Delete(T entity)
        {
            Context.Set<T>().Remove(entity);
        }

        /// <summary>
        /// Get a list of entities.
        /// </summary>
        /// <returns>The list of entities.</returns>
        public virtual IEnumerable<T> List()
        {
            return Context.Set<T>().ToList();
        }

        /// <summary>
        /// Search for entities.
        /// </summary>
        /// <param name="expression">The search expresion.</param>
        /// <returns>The set of matching entities.</returns>
        public virtual IQueryable<T> Search(Expression<Func<T, bool>> expression)
        {
            return Context.Set<T>().Where(expression);
        }

        /// <summary>
        /// Begin a transaction.
        /// </summary>
        public IDbContextTransaction BeginTransaction()
        {
            return Context.Database.BeginTransaction();
        }

        /// <summary>
        /// Save the changes made to the repository.
        /// </summary>
        public void SaveChanges()
        {
            Context.SaveChanges();
        }

        /// <summary>
        /// Dispose of resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Dispose of resources.
        /// </summary>
        /// <param name="disposing">Whether to dispose of managed resources or not.</param>
        protected virtual void Dispose(bool disposing)
        {
        }
    }
}
