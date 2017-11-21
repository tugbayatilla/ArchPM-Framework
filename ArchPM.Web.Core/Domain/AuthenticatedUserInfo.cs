using System;
using System.Collections.Generic;

namespace ArchPM.Web.Core.Domain
{
    public class AuthenticatedUserInfo
    {
        public AuthenticatedUserInfo()
        {
            this.Scenarios = new List<String>();
            this.Roles = new List<String>();
        }
        public String Username { get; set; }
        public String Mail { get; set; }
        public String Password { get; set; }
        public String Fullname { get; set; }
        public List<String> Roles { get; set; }
        public List<String> Scenarios { get; set; }
    }
}
