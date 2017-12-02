using ArchPM.Data;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ArchPM.Data.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IBusiness<T> where T : IDbEntity
    {
        /// <summary>
        /// Delete the entity by given id
        /// </summary>
        /// <param name="id">The identifier.</param>
        void Delete(int id);

        /// <summary>
        /// Gets all data
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <returns></returns>
        IEnumerable<T> GetAll(Expression<Func<T, Boolean>> predicate = null);

        /// <summary>
        /// get all valid data
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> GetAllValid();

        /// <summary>
        /// Get entity by given id
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        T GetById(int id);

        /// <summary>
        /// Persist the entity to database
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        T Save(T entity);

        /// <summary>
        /// Counts the specified predicate.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <param name="wherePredicate">The where predicate.</param>
        /// <returns></returns>
        Int32 Count(Expression<Func<T, Object>> predicate = null, Expression<Func<T, Boolean>> wherePredicate = null);

        /// <summary>
        /// Searches the specified skip.
        /// </summary>
        /// <param name="skip">The skip.</param>
        /// <param name="take">The take.</param>
        /// <param name="wherePredicate">The where predicate.</param>
        /// <param name="orderByPredicate">The order by predicate.</param>
        /// <param name="isOrderByDesc">if set to <c>true</c> [is order by desc].</param>
        /// <returns></returns>
        IEnumerable<T> Search(Int32 skip, Int32 take, Expression<Func<T, Boolean>> wherePredicate, Expression<Func<T, Object>> orderByPredicate, Boolean isOrderByDesc);
    }

}
