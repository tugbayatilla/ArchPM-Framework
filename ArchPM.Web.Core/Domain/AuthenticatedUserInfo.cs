using System;
using System.Collections.Generic;
using System.Security.Principal;

namespace ArchPM.Web.Core.Domain
{
    public class AuthenticatedUserInfo : IPrincipal
    {
        public AuthenticatedUserInfo()
        {
            this.Claims = new List<String>();
            this.Roles = new List<String>();
        }
        public AuthenticatedUserInfo(String mail) : this()
        {
            this.Mail = mail;
        }
        public String Username { get; set; }
        public String Mail { get; set; }
        public String Fullname { get; set; }
        public List<String> Roles { get; set; }
        public List<String> Claims { get; set; }

        public IIdentity Identity => new GenericIdentity(this.Mail);

        public bool IsInRole(string role) => Roles.Contains(role);
    }
}
