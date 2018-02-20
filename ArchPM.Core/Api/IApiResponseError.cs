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
    public interface IApiResponseError
    {
        /// <summary>
        /// Gets or sets the errors.
        /// </summary>
        /// <value>
        /// The errors.
        /// </value>
        List<ApiError> Errors { get; set; }
    }
}
