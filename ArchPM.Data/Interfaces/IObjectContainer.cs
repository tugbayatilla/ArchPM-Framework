using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchPM.Data
{
    /// <summary>
    /// 
    /// </summary>
    public interface IObjectContainer
    {
        /// <summary>
        /// Resolves the specified name.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        T Resolve<T>(String name = "");
        /// <summary>
        /// Resolves all.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IEnumerable<T > ResolveAll<T>();
    }
}
