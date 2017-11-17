using ArchPM.Core;
using ArchPM.Core.Extensions;
using ArchPM.Core.Extensions.Advanced;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ArchPM.Data.Factories
{
    public abstract class Factory<T>
    {
        protected readonly List<Type> allTypes;
        /// <summary>
        /// Gets Factory initialized or not
        /// </summary>
        public Boolean IsInitialized { get; protected set; }

        protected Factory()
        {
            allTypes = new List<Type>();
        }

        public virtual void Initialize(String binPath = "")
        {
            if (this.IsInitialized)
                return;

            var assemblies = Utils.GetAssembliesInDirectory(binPath);
            foreach (var item in assemblies)
            {
                try
                {
                    IEnumerable<Type> typesInAssembly = item.GetProviderTypes<T>();
                    allTypes.AddRange(typesInAssembly);
                }
                catch (Exception ex)
                {
                    if (ex is System.Reflection.ReflectionTypeLoadException)
                    {
                        var typeLoadException = ex as ReflectionTypeLoadException;
                        var loaderExceptions = typeLoadException.LoaderExceptions;
                    }
                }
            }

            this.IsInitialized = true;
        }

        

    }
}
