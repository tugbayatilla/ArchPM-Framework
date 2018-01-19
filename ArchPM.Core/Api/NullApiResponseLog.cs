using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchPM.Core.Api
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="ArchPM.Core.Api.IApiResponseLog" />
    public class NullApiResponseLog : IApiResponseLog
    {
        /// <summary>
        /// Logs the specified response.
        /// </summary>
        /// <param name="response">The response.</param>
        /// <returns></returns>
        public Task LogAsync(IApiResponse response)
        {
            return Task.FromResult(response);
        }

        /// <summary>
        /// Logs the specified response.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="response">The response.</param>
        /// <returns></returns>
        public Task LogAsync<T>(IApiResponse<T> response)
        {
            return Task.FromResult(response);
        }
    }
}
