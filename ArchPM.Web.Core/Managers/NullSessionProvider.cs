using ArchPM.Core.Session;
using ArchPM.Web.Core;

namespace ArchPM.Web.Core.Managers
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="ArchPM.Core.Session.ISessionProvider" />
    public class NullSessionProvider : ISessionProvider
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
                return new AuthenticatedUserInfo() { Username = "Null Session", Fullname = "This is Null Session Provider" };
            }
        }

    }
}
