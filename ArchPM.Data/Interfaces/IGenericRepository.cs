using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ArchPM.Data
{
    public interface IGenericRepository<T> : IRepository
    {
        IEnumerable<T> Find(Expression<Func<T, Boolean>> selectPredicate = null);
        T FindOne(Expression<Func<T, Boolean>> selectPredicate);
        Int32 Count(Expression<Func<T, Object>> selectPredicate = null, Expression < Func<T, Boolean>> wherePredicate = null);
        T Insert(T entity);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="wherePredicate">Where condition</param>
        /// <param name="givenPredicate">the properties which are going to be updated</param>
        /// <returns></returns>
        T Update(T entity, Expression<Func<T, Boolean>> wherePredicate = null, Expression<Func<T, Object>> givenPredicate= null);
    }

}
