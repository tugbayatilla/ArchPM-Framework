using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace ArchPM.Core
{
    /// <summary>
    /// 
    /// </summary>
    public static class AssemblyManager
    {
        /// <summary>
        /// Loads the assemblies.
        /// </summary>
        /// <param name="directoryFolderPath">The directory folder path.</param>
        /// <returns></returns>
        public static IEnumerable<Assembly> GetAssembliesInDirectory(String directoryPath = "", SearchOption searchOption = SearchOption.TopDirectoryOnly)
        {
            if (String.IsNullOrEmpty(directoryPath))
                directoryPath = System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "bin"); // note: don't use CurrentEntryAssembly or anything like that.

            DirectoryInfo directory = new DirectoryInfo(directoryPath);
            if (directory.Exists)
            {
                FileInfo[] files = directory.GetFiles("*.dll", searchOption);

                foreach (FileInfo file in files)
                {
                    // Load the file into the application domain.
                    AssemblyName assemblyName = AssemblyName.GetAssemblyName(file.FullName);
                    Assembly assembly = AppDomain.CurrentDomain.Load(assemblyName);
                    yield return assembly;
                }
            }

            yield break;
        }

        /// <summary>
        /// Gets the provider.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="currentAssembly">The current assembly.</param>
        /// <returns></returns>
        public static IEnumerable<T> GetProvider<T>(this Assembly currentAssembly)
        {
            var types = currentAssembly.GetTypes();
            foreach (var type in types)
            {
                if (!type.IsClass || type.IsNotPublic) continue;
                if (type.GetInterfaces().Contains(typeof(T)))
                {
                    T provider = default(T);
                    try
                    {
                        provider = (T)Activator.CreateInstance(type);
                    }
                    catch { }

                    if(provider != null)
                        yield return provider;
                }
            }

            yield break;
        }

    }
}
