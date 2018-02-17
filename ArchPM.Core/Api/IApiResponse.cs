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
    public interface IApiResponse : IApiResponseElapsedTime
    {
        /// <summary>
        /// Gets or sets the requested URL.
        /// </summary>
        /// <value>
        /// The requested URL.
        /// </value>
        String RequestedUrl { get; set; }
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
        /// Gets or sets the message.
        /// </summary>
        /// <value>
        /// The code.
        /// </value>
        String Message { get; set; }
        /// <summary>
        /// Gets the source.
        /// </summary>
        /// <value>
        /// The code.
        /// </value>
        String Source { get; }

        /// <summary>
        /// Gets or sets the try count.
        /// </summary>
        /// <value>
        /// The try count.
        /// </value>
        Int32 TryCount { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public interface IApiResponse<T> : IApiResponse
    {
        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value>
        /// The data.
        /// </value>
        T Data { get; set; }
    }

}
