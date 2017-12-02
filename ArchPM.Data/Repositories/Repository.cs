//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Linq;
//using ArchPM.Core.Exceptions;
//using System.Linq.Expressions;
//using ArchPM.Data;

//namespace Sisli.MIS.Infrastructure
//{
//    /// <summary>
//    /// 
//    /// </summary>
//    /// <typeparam name="T"></typeparam>
//    /// <seealso cref="ArchPM.Data.IRepository{T}" />
//    public abstract class Repository<T> : IRepository<T>
//        where T : IDbEntity, new()
//    {
//        /// <summary>
//        /// The context
//        /// </summary>
//        protected readonly IDbContext context;
//        /// <summary>
//        /// Gets or sets a value indicating whether this instance is tracable.
//        /// </summary>
//        /// <value>
//        ///   <c>true</c> if this instance is tracable; otherwise, <c>false</c>.
//        /// </value>
//        public Boolean IsTracable { get; set; }


//        /// <summary>
//        /// Initializes a new instance of the <see cref="Repository{T}"/> class.
//        /// </summary>
//        /// <param name="context">The context.</param>
//        public Repository(IDbContext context)
//        {
//            this.context = context;
//            this.IsTracable = false;
//        }

//        /// <summary>
//        /// Finds the specified select predicate.
//        /// </summary>
//        /// <param name="wherePredicate">The select predicate.</param>
//        /// <returns></returns>
//        public abstract IEnumerable<T> Find(Expression<Func<T, bool>> wherePredicate = null);
//        /// <summary>
//        /// Finds the one.
//        /// </summary>
//        /// <param name="wherePredicate">The select predicate.</param>
//        /// <returns></returns>
//        public abstract T FindOne(Expression<Func<T, bool>> wherePredicate);
//        /// <summary>
//        /// Counts the specified select predicate.
//        /// </summary>
//        /// <param name="selectPredicate">The select predicate.</param>
//        /// <param name="wherePredicate">The where predicate.</param>
//        /// <returns></returns>
//        public abstract int Count(Expression<Func<T, object>> selectPredicate = null, Expression<Func<T, bool>> wherePredicate = null);
//        /// <summary>
//        /// Inserts the specified entity.
//        /// </summary>
//        /// <param name="entity">The entity.</param>
//        /// <returns></returns>
//        public abstract T Insert(T entity);
//        /// <summary>
//        /// Updates the specified entity.
//        /// </summary>
//        /// <param name="entity">The entity.</param>
//        /// <param name="wherePredicate">Where condition</param>
//        /// <param name="givenPredicate">the properties which are going to be updated</param>
//        /// <returns></returns>
//        public abstract T Update(T entity, Expression<Func<T, bool>> wherePredicate = null, Expression<Func<T, object>> givenPredicate = null);
//    }

//}
