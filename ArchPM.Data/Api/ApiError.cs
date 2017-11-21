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
    public class ApiError
    {
        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        public String Message { get; set; }
        /// <summary>
        /// Gets or sets the detailed message.
        /// </summary>
        /// <value>
        /// The detailed message.
        /// </value>
        public String DetailedMessage { get; set; }
    }
}
