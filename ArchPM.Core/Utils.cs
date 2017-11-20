using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace ArchPM.Core
{
    /// <summary>
    /// 
    /// </summary>
    public static class Utils
    {
        /// <summary>
        /// The lock
        /// </summary>
        static Object _lock = new object();

        /// <summary>
        /// Creates the unique number.
        /// </summary>
        /// <returns></returns>
        public static UInt64 CreateUniqueNumber()
        {
            UInt64 result = default(UInt64);
            lock (_lock)
            {
                var unique = String.Format("{0:yyyyMMddHHmmssffffff}", DateTime.Now);
                result = Convert.ToUInt64(unique);
            }
            return result;
        }

        /// <summary>
        /// Loads the assemblies.
        /// </summary>
        /// <param name="directoryPath">The directory path.</param>
        /// <param name="searchOption">The search option.</param>
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

    }
}
