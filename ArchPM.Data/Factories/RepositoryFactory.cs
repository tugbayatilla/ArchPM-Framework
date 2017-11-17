//using ArchPM.Data;
//using System;
//using System.Linq;
//using System.Reflection;
//using ArchPM.Core.Extensions;
//using System.Collections.Generic;
//using ArchPM.Core.Exceptions;
//using ArchPM.Core.Attributes;
//using System.IO;
//using ArchPM.Core.Enums;

//namespace Sisli.MIS.Infrastructure.Factories
//{
//    //closed
//    public sealed class RepositoryFactory : Factory<IRepository>
//    {
//        public static readonly RepositoryFactory Instance = new RepositoryFactory();

//        private RepositoryFactory()
//            : base()
//        {
//        }


//        /// <summary>
//        /// Gets the type of the data.
//        /// </summary>
//        /// <value>
//        /// The type of the data.
//        /// </value>
//        public ApplicationDataTypes DataType
//        {
//            get
//            {
//                if (!dataType.HasValue)
//                {
//                    String dataTypeDefinedInConfig = AppConfigManager.GetConfig("Repository.DataType", defaultValue: ApplicationDataTypes.Ado.ToString());
//                    dataType = EnumManager<ApplicationDataTypes>.Parse(dataTypeDefinedInConfig);
//                }
//                return dataType.Value;
//            }
//        }
//        ApplicationDataTypes? dataType;


//        /// <summary>
//        /// Creates the specified context.
//        /// </summary>
//        /// <typeparam name="T"></typeparam>
//        /// <param name="context">The context.</param>
//        /// <returns></returns>
//        /// <exception cref="DataLayerException">
//        /// RepositoryFactory must be initialized first! Call Initialize method!
//        /// or
//        /// Repository '{0}' not found in any assemblies!
//        /// </exception>
//        public Repository<T> Create<T>(IDbContext context) where T : IDbEntity, new()
//        {
//            if (!IsInitialized)
//                throw new DataLayerException($"{this.GetType().Name} must be initialized first! Call Initialize method!");


//            //general or specialized all repositories with same 
//            var repositoryType = allTypes
//                .FirstOrDefault(p => p.GetCustomAttributes<RepositoryInfoAttribute>()
//                .Any(x => x.DataType == this.DataType
//                       && x.RepositoryType == RepositoryTypes.General));

//            if (repositoryType == null)
//                throw new DataLayerException($"Repository ''{this.DataType}'' with ''{RepositoryTypes.General.GetName()}'' not found in any assemblies!");

//            var repositoryInstance = repositoryType.CreateInstanceAndCast<Repository<T>>(context);

//            return repositoryInstance;
//        }

//        /// <summary>
//        /// Creates the custom class' implementing given repository interfaces as T
//        /// </summary>
//        /// <typeparam name="T">Custom repository interface</typeparam>
//        /// <param name="context">The database context coming from unitofwork</param>
//        /// <returns>new instance of class implementing given repository interface</returns>
//        /// <exception cref="DataLayerException">
//        /// RepositoryFactory must be initialized first! Call Initialize method!
//        /// or
//        /// Repository '{0}' not found in any assemblies!
//        /// </exception>
//        public T CreateCustom<T>(IDbContext context) where T : IRepository
//        {
//            if (!IsInitialized)
//                throw new DataLayerException("RepositoryFactory must be initialized first! Call Initialize method!");

//            //general or specialized all repositories with same 
//            var repositoryType = allTypes
//                .FirstOrDefault(p => p.GetCustomAttributes<RepositoryInfoAttribute>().Any(x => x.DataType == this.DataType && x.RepositoryType == RepositoryTypes.Custom)
//                && p.GetInterfaces().Any(x => x == typeof(T)));

//            if (repositoryType == null)
//                throw new DataLayerException($"Repository ''{this.DataType.GetName()}'' with ''{RepositoryTypes.Custom.GetName()}'' not found in any assemblies!");

//            var repositoryInstance = repositoryType.CreateInstanceAndCast<T>(context);

//            return repositoryInstance;

//        }

//    }
//}
