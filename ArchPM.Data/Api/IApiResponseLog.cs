using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchPM.Data.Api
{
    /// <summary>
    /// 
    /// </summary>
    public interface IApiResponseLog
    {
        /// <summary>
        /// Logs the specified response.
        /// </summary>
        /// <param name="response">The response.</param>
        /// <returns></returns>
        Task LogAsync(IApiResponse response);

        /// <summary>
        /// Logs the specified response.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="response">The response.</param>
        /// <returns></returns>
        Task LogAsync<T>(IApiResponse<T> response);
    }
}
