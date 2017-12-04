using ArchPM.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Web.Script.Serialization;

namespace ArchPM.Core.Session
{
    /// <summary>
    /// 
    /// </summary>
    public class AuthenticatedUserInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticatedUserInfo" /> class.
        /// </summary>
        public AuthenticatedUserInfo()
        {
            this.Claims = new List<String>();
            this.Roles = new List<String>();
            this.Data = new Dictionary<String, object>
            {
                { "ID", this.ID }
            };

            this.Mail = "";
        }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Object ID { get { return _id; } set { _id = value; this.Data.Save("ID", value); } }
        Object _id;
        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        /// <value>
        /// The username.
        /// </value>
        public String Username { get; set; }
        /// <summary>
        /// Gets or sets the mail.
        /// </summary>
        /// <value>
        /// The mail.
        /// </value>
        public String Mail { get; set; }
        /// <summary>
        /// Gets or sets the fullname.
        /// </summary>
        /// <value>
        /// The fullname.
        /// </value>
        public String Fullname { get; set; }
        /// <summary>
        /// Gets or sets the roles.
        /// </summary>
        /// <value>
        /// The roles.
        /// </value>
        public List<String> Roles { get; set; }
        /// <summary>
        /// Gets or sets the claims.
        /// </summary>
        /// <value>
        /// The claims.
        /// </value>
        public List<String> Claims { get; set; }
        /// <summary>
        /// Gets or sets the claims.
        /// </summary>
        /// <value>
        /// The claims.
        /// </value>
        public IDictionary<String, Object> Data { get; set; }

    }
}
