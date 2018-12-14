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
        static readonly Object _lock = new object();

        /// <summary>
        /// Creates the unique number.
        /// </summary>
        /// <returns></returns>
        public static UInt64 CreateUniqueNumber(DateTime? now = null)
        {
            if(!now.HasValue)
            {
                now = DateTime.Now;
            }

            UInt64 result = default(UInt64);
            lock (_lock)
            {
                var unique = String.Format("{0:yyyyMMddHHmmssffffff}", now);
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

        /// <summary>
        /// Checks the path is a directory
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>
        /// True: Directory, False:File
        /// </returns>
        public static Boolean IsDirectory(String path)
        {
            //false means it's a file
            Boolean result = false;
            // get the file attributes for file or directory
            FileAttributes attr = File.GetAttributes(path);

            //detect whether its a directory
            if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
            {
                result = true;
            }

            return result;
        }

    }
}
