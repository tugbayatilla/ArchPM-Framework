using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ArchPM.Data
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="ArchPM.Data.IRepository" />
    public interface IGenericRepository<T> : IRepository
    {
        /// <summary>
        /// Finds the specified select predicate.
        /// </summary>
        /// <param name="selectPredicate">The select predicate.</param>
        /// <returns></returns>
        IEnumerable<T> Find(Expression<Func<T, Boolean>> selectPredicate = null);
        /// <summary>
        /// Finds the one.
        /// </summary>
        /// <param name="selectPredicate">The select predicate.</param>
        /// <returns></returns>
        T FindOne(Expression<Func<T, Boolean>> selectPredicate);
        /// <summary>
        /// Counts the specified select predicate.
        /// </summary>
        /// <param name="selectPredicate">The select predicate.</param>
        /// <param name="wherePredicate">The where predicate.</param>
        /// <returns></returns>
        Int32 Count(Expression<Func<T, Object>> selectPredicate = null, Expression < Func<T, Boolean>> wherePredicate = null);
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
        /// <param name="wherePredicate">Where condition</param>
        /// <param name="givenPredicate">the properties which are going to be updated</param>
        /// <returns></returns>
        T Update(T entity, Expression<Func<T, Boolean>> wherePredicate = null, Expression<Func<T, Object>> givenPredicate= null);
    }

}
