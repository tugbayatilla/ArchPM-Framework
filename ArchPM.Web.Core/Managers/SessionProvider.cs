using ArchPM.Core;
using ArchPM.Core.Exceptions;
using ArchPM.Core.Session;
using System.Web;

namespace ArchPM.Web.Core.Managers
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="ArchPM.Core.Session.ISessionProvider" />
    public class SessionProvider : ISessionProvider
    {
        /// <summary>
        /// The current context
        /// </summary>
        readonly HttpContextBase currentContext;
        /// <summary>
        /// Initializes a new instance of the <see cref="SessionProvider" /> class.
        /// </summary>
        /// <param name="currentContext">The current context.</param>
        public SessionProvider(HttpContextBase currentContext)
        {
            currentContext.ThrowExceptionIfNull();
            this.currentContext = currentContext;
        }


        /// <summary>
        /// Gets the authentication user. Need to set &lt;pages pageBaseType="ArchPM.Web.Core.BaseViewPage"&gt;
        /// </summary>
        /// <value>
        /// The authentication user.
        /// </value>
        /// <exception cref="AuthenticationException">Context User is not AuthenticatedUserInfo</exception>
        public AuthenticatedUserInfo AuthUser
        {
            get
            {
                if (currentContext.User is AuthenticatedUserInfo)
                {
                    var result = currentContext.User as AuthenticatedUserInfo;
                    return result;
                }
                else
                {
                    throw new AuthenticationException("Context User is not AuthenticatedUserInfo");
                }
            }
        }
    }
}
