using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ArchPM.Data
{
    /// <summary>
    /// Common Database Operations
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="ArchPM.Data.IRepository" />
    public interface IRepository<T> : IRepository
    {
        /// <summary>
        /// Sets the context.
        /// </summary>
        /// <param name="context">The context.</param>
        void SetContext(IDbContext context);

        /// <summary>
        /// Finds the specified select predicate.
        /// </summary>
        /// <param name="wherePredicate">The where predicate.</param>
        /// <returns></returns>
        IEnumerable<T> Find(Expression<Func<T, Boolean>> wherePredicate);
        /// <summary>
        /// Finds the specified select predicate.
        /// </summary>
        /// <param name="selectPredicate">The select predicate.</param>
        /// <param name="wherePredicate">The where predicate.</param>
        /// <returns></returns>
        IEnumerable<T> Find(Expression<Func<T, Boolean>> selectPredicate, Expression<Func<T, Boolean>> wherePredicate);
        /// <summary>
        /// Finds the one.
        /// </summary>
        /// <param name="wherePredicate">The where predicate.</param>
        /// <returns></returns>
        T FindOne(Expression<Func<T, Boolean>> wherePredicate);
        /// <summary>
        /// Counts the specified select predicate.
        /// </summary>
        /// <returns></returns>
        Int32 Count();
        /// <summary>
        /// Counts the specified where predicate.
        /// </summary>
        /// <param name="wherePredicate">The where predicate.</param>
        /// <returns></returns>
        Int32 Count(Expression<Func<T, Boolean>> wherePredicate = null);
        /// <summary>
        /// Counts the specified select predicate.
        /// </summary>
        /// <param name="selectPredicate">The select predicate.</param>
        /// <param name="wherePredicate">The where predicate.</param>
        /// <returns></returns>
        Int32 Count(Expression<Func<T, Object>> selectPredicate, Expression<Func<T, Boolean>> wherePredicate);
        /// <summary>
        /// Inserts the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        T Insert(T entity);
        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="selectPredicate">The where predicate.</param>
        /// <returns></returns>
        T Update(T entity, Expression<Func<T, Object>> selectPredicate = null);

    }

    /// <summary>
    /// required for reflection
    /// </summary>
    public interface IRepository
    {
    }
}
