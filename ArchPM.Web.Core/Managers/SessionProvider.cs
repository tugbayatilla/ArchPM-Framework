using ArchPM.Core.Exceptions;
using ArchPM.Core.Session;

namespace ArchPM.Web.Core.Managers
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="ArchPM.Core.Session.ISessionProvider" />
    public class SessionProvider : ISessionProvider
    {
        /// <summary>
        /// Gets the authentication user.
        /// </summary>
        /// <value>
        /// The authentication user.
        /// </value>
        public AuthenticatedUserInfo AuthUser
        {
            get
            {
                return GetAuthenticatedUserInfo();
            }
        }

        /// <summary>
        /// Gets the authenticated user information.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="AuthenticationException">Authentication Failed!</exception>
        public static AuthenticatedUserInfo GetAuthenticatedUserInfo()
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
