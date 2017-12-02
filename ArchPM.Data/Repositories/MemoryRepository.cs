//using ArchPM.Data;
//using ArchPM.Data.Contexts;
//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Linq;
//using System.Linq.Expressions;

//namespace Sisli.MIS.Infrastructure.Repositories
//{
//    /// <summary>
//    /// 
//    /// </summary>
//    /// <typeparam name="T"></typeparam>
//    /// <seealso cref="Sisli.MIS.Infrastructure.Repository{T}" />
//    public class MemoryRepository<T> : Repository<T> where T : IDbEntity, new()
//    {
//        /// <summary>
//        /// Initializes a new instance of the <see cref="MemoryRepository{T}"/> class.
//        /// </summary>
//        /// <param name="context">The context.</param>
//        public MemoryRepository(IDbContext context)
//            : base(context)
//        {
//        }

//        public override int Count(Expression<Func<T, object>> selectPredicate = null, Expression<Func<T, bool>> wherePredicate = null)
//        {
//            var data = MemoryContext.database.Where(p => p.GetType() == typeof(T)).Cast<T>();
//            if (wherePredicate != null)
//                data = data.Where(wherePredicate.Compile());

//            return data.Count();
//        }

//        public override IEnumerable<T> Find(Expression<Func<T, bool>> wherePredicate = null)
//        {
//            var data = MemoryContext.database.Where(p => p.GetType() == typeof(T)).Cast<T>();
//            if (wherePredicate != null)
//                data = data.Where(wherePredicate.Compile());

//            return data;
//        }

//        public override T FindOne(Expression<Func<T, bool>> wherePredicate)
//        {
//            return Find(wherePredicate).FirstOrDefault();
//        }

//        public override T Insert(T entity)
//        {
//            MemoryContext.database.Add(entity);
//            return entity;
//        }

//        public override T Update(T entity, Expression<Func<T, bool>> wherePredicate = null, Expression<Func<T, object>> givenPredicate = null)
//        {
//            var temp = MemoryContext.database.Single(p => p.ID == entity.ID);
//            MemoryContext.database.Remove(temp);
//            MemoryContext.database.Add(entity);
//            return entity;
//        }

//    }
//}
