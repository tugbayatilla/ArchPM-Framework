using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Web.Script.Serialization;

namespace ArchPM.Core.Session
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="ArchPM.Core.Session.AuthenticatedUserInfo" />
    public class NullAuthenticatedUserInfo : AuthenticatedUserInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticatedUserInfo" /> class.
        /// </summary>
        /// <param name="ex">The ex.</param>
        public NullAuthenticatedUserInfo(Exception ex) : base()
        {
            Username = "";
            Fullname = "";
            this.Exception = ex;
        }

        /// <summary>
        /// Gets or sets the exception.
        /// </summary>
        /// <value>
        /// The exception.
        /// </value>
        public Exception Exception { get; set; }


    }
}
