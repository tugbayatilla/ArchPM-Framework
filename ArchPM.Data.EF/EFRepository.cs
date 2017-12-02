//using System;
//using System.Collections.Generic;
//using System.Data.Entity;
//using System.Linq;
//using System.Linq.Expressions;
//using System.Text;
//using System.Threading.Tasks;

//namespace ArchPM.Data.EF
//{
//    public class EFRepository<T> : IRepository<T>
//    {
//        DbContext context;
//        public EFRepository(DbContext context)
//        {
//            this.context = context;
//        }

//        public int Count()
//        {
//            context.Set<>
//        }

//        public int Count(Expression<Func<T, bool>> wherePredicate = null)
//        {
//            throw new NotImplementedException();
//        }

//        public int Count(Expression<Func<T, object>> selectPredicate, Expression<Func<T, bool>> wherePredicate)
//        {
//            throw new NotImplementedException();
//        }

//        public IEnumerable<T> Find(Expression<Func<T, bool>> wherePredicate)
//        {
//            throw new NotImplementedException();
//        }

//        public IEnumerable<T> Find(Expression<Func<T, bool>> selectPredicate, Expression<Func<T, bool>> wherePredicate)
//        {
//            throw new NotImplementedException();
//        }

//        public T FindOne(Expression<Func<T, bool>> wherePredicate)
//        {
//            throw new NotImplementedException();
//        }

//        public T Insert(T entity)
//        {
//            throw new NotImplementedException();
//        }

//        public void SetContext(IDbContext context)
//        {
//            throw new NotImplementedException();
//        }

//        public T Update(T entity, Expression<Func<T, object>> selectPredicate = null)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
