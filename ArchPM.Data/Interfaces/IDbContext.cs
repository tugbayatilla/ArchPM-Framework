using System;
using System.Data;

namespace ArchPM.Data
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    public interface IDbContext : IDisposable
    {
        //creates connection internally
        /// <summary>
        /// Creates the unit of work.
        /// </summary>
        /// <returns></returns>
        IUnitOfWork CreateUnitOfWork();
        /// <summary>
        /// Creates the command.
        /// </summary>
        /// <returns></returns>
        IDbCommand CreateCommand();
    }
}
