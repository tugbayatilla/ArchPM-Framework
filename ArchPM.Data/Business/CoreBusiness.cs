//using ArchPM.Core;
//using ArchPM.Core.Exceptions;
//using ArchPM.Core.Session;
//using ArchPM.Data.Interfaces;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Linq.Expressions;



//namespace ArchPM.Data.Business
//{
//    public class CoreBusiness<To> : IBusiness<To> where To : class, IDbEntity, new()
//    {
//        /// <summary>
//        /// The session provider
//        /// </summary>
//        protected readonly ISessionProvider sessionProvider;
//        /// <summary>
//        /// The container
//        /// </summary>
//        protected readonly IObjectContainer container;
//        /// <summary>
//        /// Gets or sets the database context.
//        /// </summary>
//        /// <value>
//        /// The database context.
//        /// </value>
//        public IDbContext DbContext { get; set; }

//        /// <summary>
//        /// Initializes a new instance of the <see cref="CoreBusiness{To}"/> class.
//        /// </summary>
//        /// <param name="container">The container.</param>
//        public CoreBusiness(IObjectContainer container)
//        {
//            container.ThrowExceptionIfNull();
//            this.container = container;
//            this.DbContext = container.Resolve<IDbContext>();
//        }

//        public IEnumerable<To> GetAll(Expression<Func<To, Boolean>> predicate = null)
//        {
//            var rep = container.Resolve<IRepository<To>>();
//            rep.SetContext(this.DbContext);
//            return rep.Find(predicate);
//        }

//        public Int32 Count(Expression<Func<To, Object>> predicate = null, Expression<Func<To, Boolean>> wherePredicate = null)
//        {
//            var rep = container.Resolve<IRepository<To>>();
//            return rep.Count(predicate, wherePredicate);
//        }

//        public IEnumerable<To> GetAllValid()
//        {
//            var rep = container.Resolve<IRepository<To>>();
//            var result = rep.Find(p => p.STATUS == EntityStatus.Active);

//            return result;
//        }

//        public To GetById(int id)
//        {
//            var rep = container.Resolve<IRepository<To>>();
//            var item = rep.FindOne(p => p.ID == id);
//            return item;
//        }

//        /// <summary>
//        /// Changes STATUS to Deleted
//        /// </summary>
//        /// <param name="id"></param>
//        public void Delete(int id)
//        {
//            try
//            {
//                var rep = container.Resolve<IRepository<To>>();
//                var entity = rep.FindOne(p => p.ID == id);
//                entity.STATUS = EntityStatus.Deleted;
//                rep.Update(entity, p => p.STATUS);

//                //changeLogService.ChangeLog(entity, this.dbContext, this.sessionProvider, ChangeLogTypes.Delete);
//            }
//            catch (Exception ex)
//            {
//                throw new BusinessException("Delete Failed!", ex);
//            }

//        }

//        /// <summary>
//        /// Persist the entity to database
//        /// </summary>
//        /// <param name="entity">The entity.</param>
//        /// <returns></returns>
//        /// <exception cref="BusinessException">Save Failed!</exception>
//        public To Save(To entity)
//        {
//            try
//            {
//                var rep = container.Resolve<IRepository<To>>(); //context verilecek

//                if (entity.ID == default(Int32))
//                {
//                    entity.PROCESS_USER_NAME = sessionProvider.AuthUser.Username;
//                    entity = rep.Insert(entity);
//                }
//                else
//                {
//                    rep.Update(entity);
//                }

//                return entity;
//            }
//            catch (Exception ex)
//            {
//                throw new BusinessException("Save Failed!", ex);
//            }

//        }


//    }
//}
