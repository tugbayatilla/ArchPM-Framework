//using ArchPM.Core;
//using ArchPM.Core.Exceptions;
//using ArchPM.Data.Interfaces;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Linq.Expressions;



//namespace ArchPM.Data.Business
//{
//    public class CoreBusiness<To> : IBusiness<To> where To : class, IDbEntity, new()
//    {
//        protected readonly ISessionProvider
//        protected readonly IObjectContainer container;
//        public IDbContext DbContext { get; set; }

//        public CoreBusiness(IObjectContainer container)
//        {
//            container.ThrowExceptionIfNull();
//            this.container = container;
//        }

//        public IEnumerable<To> GetAll(Expression<Func<To, Boolean>> predicate = null)
//        {
//            var rep = container.Resolve<IRepository<To>>();
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

//        public To Save(To entity)
//        {
//            try
//            {
//                var rep = container.Resolve<IRepository<To>>(); //context verilecek
//                //ChangeLogTypes changeLogType = ChangeLogTypes.Insert;

//                if (entity.ID == default(Int32))
//                {
//                    entity.PROCESS_USER_NAME = sessionProvider.AuthUser.Username;
//                    entity = rep.Insert(entity);
//                }
//                else
//                {
//                    rep.Update(entity);
//                    changeLogType = ChangeLogTypes.Update;
//                }

//                //insert change log
//                changeLogService.ChangeLog(entity, this.dbContext, this.sessionProvider, changeLogType);

//                return entity;
//            }
//            catch (Exception ex)
//            {
//                throw new BusinessException("Save Failed!", ex);
//            }

//        }

//        public void UpdateGiven(To entity, Expression<Func<To, Object>> includePredicate)
//        {
//            try
//            {
//                var rep = RepositoryFactory.Instance.Create<To>(this.dbContext);
//                entity = rep.UpdateGivenFields(entity, includePredicate);

//                //insert change log
//                changeLogService.ChangeLog(entity, this.dbContext, this.sessionProvider, ChangeLogTypes.Update);
//            }
//            catch (Exception ex)
//            {
//                throw new BusinessException("UpdateGiven Failed!", ex);
//            }

//        }



//        public IEnumerable<To> Search(int skip, int take, Expression<Func<To, bool>> wherePredicate = null, OrderByPredicateContainer<To> orderByContainer = null)
//        {
//            try
//            {
//                var rep = RepositoryFactory.Instance.Create<To>(this.dbContext);
//                return rep.Search(skip, take, wherePredicate, orderByContainer);
//            }
//            catch (Exception ex)
//            {
//                throw new BusinessException("Search Failed!", ex);
//            }
//        }

//    }
//}
