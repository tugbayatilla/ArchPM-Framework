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
    public interface IApiResponse
    {
        /// <summary>
        /// Gets or sets a value the requested operation whether is operated correctly or not. 
        /// this is not HttpResponse result.
        /// </summary>
        /// <value>
        ///   <c>true</c> if result; otherwise, <c>false</c>.
        /// </value>
        Boolean Result { get; set; }
        /// <summary>
        /// Gets or sets the application specific code.
        /// </summary>
        /// <value>
        /// The code.
        /// </value>
        String Code { get; set; }
        /// <summary>
        /// Gets or sets the errors.
        /// </summary>
        /// <value>
        /// The errors.
        /// </value>
        List<ApiError> Errors { get; set; }
        /// <summary>
        /// Gets or sets the et.
        /// </summary>
        /// <value>
        /// The Execution Time
        /// </value>
        Double ET { get; set; }
    }
}
