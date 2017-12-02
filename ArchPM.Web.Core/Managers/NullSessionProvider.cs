using ArchPM.Core.Session;
using ArchPM.Web.Core;

namespace ArchPM.Web.Core.Managers
{
    public class NullSessionProvider : ISessionProvider
    {
        public AuthenticatedUserInfo AuthUser
        {
            get
            {
                return getAuthUser();
            }
        }

        public static AuthenticatedUserInfo getAuthUser()
        {
            return new AuthenticatedUserInfo() { Username = "Null Session", Fullname = "This is Null Session Provider" };
        }


    }
}
