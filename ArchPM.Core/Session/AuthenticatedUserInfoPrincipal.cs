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
    /// <seealso cref="System.Security.Principal.IPrincipal" />
    public class AuthenticatedUserInfoPrincipal : AuthenticatedUserInfo, IPrincipal
    {
        IIdentity identity;


        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticatedUserInfo" /> class.
        /// </summary>
        /// <param name="info">The information.</param>
        public AuthenticatedUserInfoPrincipal(AuthenticatedUserInfo info) : base()
        {
            this.Claims = info.Claims;
            this.Fullname = info.Fullname;
            this.Mail = String.IsNullOrEmpty(info.Mail) ? "" : info.Mail;
            this.Roles = info.Roles;
            this.Username = info.Username;
            this.identity = new GenericIdentity(Mail);
        }

        /// <summary>
        /// Gets the identity of the current principal.
        /// </summary>
        public IIdentity Identity => identity;

        /// <summary>
        /// Determines whether the current principal belongs to the specified role.
        /// </summary>
        /// <param name="role">The name of the role for which to check membership.</param>
        /// <returns>
        /// true if the current principal is a member of the specified role; otherwise, false.
        /// </returns>
        public bool IsInRole(string role) => Roles.Contains(role);

        /// <summary>
        /// Sets the identity.
        /// </summary>
        /// <param name="identity">The identity.</param>
        public void SetIdentity(IIdentity identity)
        {
            this.identity = identity;
        }
    }
}
