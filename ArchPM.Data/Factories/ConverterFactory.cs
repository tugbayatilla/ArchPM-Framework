//using ArchPM.Core.Attributes;
//using ArchPM.Core.Exceptions;
//using ArchPM.Core.Extensions;
//using ArchPM.Data;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Reflection;
//using System.Text;
//using System.Threading.Tasks;

//namespace X
//{
//    public sealed class ConverterFactory : Factory<IObjectConverter>
//    {
//        public static readonly ConverterFactory Instance = new ConverterFactory();

//        private ConverterFactory() : base()
//        {
//        }

//        public IObjectConverter<From, To> Create<From, To>() 
//            where From : class, new() 
//            where To : class, new()
//        {
//            if (!IsInitialized)
//                throw new DataLayerException("ConverterFactory must be initialized first! Call Initialize method!");

//            //general or specialized all repositories with same 
//            var repositoryType = allTypes
//                .FirstOrDefault(p => p.GetCustomAttributes<ConverterInfoAttribute>()
//                .Any(x => x.FromType == typeof(From) && x.ToType == typeof(To)));

//            //
//            if (repositoryType == null)
//                repositoryType = typeof(GeneralObjectConverter<From, To>);

//            var repositoryInstance = repositoryType.CreateInstanceAndCast<IObjectConverter<From, To>>();

//            return repositoryInstance;

//        }
//    }
//}
