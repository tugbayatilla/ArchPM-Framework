using ArchPM.Core.Exceptions;
using ArchPM.Core.Session;

namespace ArchPM.Web.Core.Managers
{
    public class SessionProvider : ISessionProvider
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
            //eachtime it must be read from the cookie. if we set into the variable then sessionprovider keeps it.
            //kernel keeps it as static.
            var authUser = CookieManager.GetAuthUser();
            if (authUser == null || authUser is NullAuthenticatedUserInfo)
                throw new AuthenticationException("Authentication Failed!");

            return authUser;
        }


    }
}
