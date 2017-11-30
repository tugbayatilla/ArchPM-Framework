using System;

namespace ArchPM.Web.Core.Domain
{
    public class NullAuthenticatedUserInfo : AuthenticatedUserInfo
    {
        public NullAuthenticatedUserInfo(Exception ex) : base("")
        {
            this.Exception = ex;
        }

        public Exception Exception { get; set; }
    }
}
