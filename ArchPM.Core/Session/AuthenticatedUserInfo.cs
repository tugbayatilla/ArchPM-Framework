using System;
using System.Collections.Generic;
using System.Security.Principal;

namespace ArchPM.Core.Domain.Session
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Security.Principal.IPrincipal" />
    public class AuthenticatedUserInfo : IPrincipal
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticatedUserInfo"/> class.
        /// </summary>
        public AuthenticatedUserInfo()
        {
            this.Claims = new List<String>();
            this.Roles = new List<String>();
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticatedUserInfo"/> class.
        /// </summary>
        /// <param name="mail">The mail.</param>
        public AuthenticatedUserInfo(String mail) : this()
        {
            this.Mail = mail;
        }
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
        /// Gets the identity of the current principal.
        /// </summary>
        public IIdentity Identity => new GenericIdentity(this.Mail);

        /// <summary>
        /// Determines whether the current principal belongs to the specified role.
        /// </summary>
        /// <param name="role">The name of the role for which to check membership.</param>
        /// <returns>
        /// true if the current principal is a member of the specified role; otherwise, false.
        /// </returns>
        public bool IsInRole(string role) => Roles.Contains(role);
    }
}
