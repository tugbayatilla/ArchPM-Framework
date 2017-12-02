using ArchPM.Core.Session;
using System;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace ArchPM.Web.Core.Managers
{
    /// <summary>
    /// 
    /// </summary>
    public class CookieManager
    {
        /// <summary>
        /// Creates the Cookie and set it to Response
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        public static HttpCookie Create(AuthenticatedUserInfo user)
        {
            HttpCookie cookie;
            DateTime cookieIssuedDate = DateTime.Now;

            var ticket = new FormsAuthenticationTicket(1,
                user.Mail,
                cookieIssuedDate,
                cookieIssuedDate.AddMinutes(FormsAuthentication.Timeout.TotalMinutes == 0 ? 15 : FormsAuthentication.Timeout.TotalMinutes),
                false,
                new JavaScriptSerializer().Serialize(user),
                FormsAuthentication.FormsCookiePath);

            String encryptedCookieContent = FormsAuthentication.Encrypt(ticket);

            cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedCookieContent)
            {
                Expires = ticket.Expiration,
                Domain = FormsAuthentication.CookieDomain,
                Path = FormsAuthentication.FormsCookiePath,
                HttpOnly = true,
                Secure = FormsAuthentication.RequireSSL
            };

            if (HttpContext.Current != null)
            {
                HttpContext.Current.Response.Cookies.Clear();
                HttpContext.Current.Response.SetCookie(cookie);
            }

            return cookie;
        }

        /// <summary>
        /// Gets the authentication user.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.NullReferenceException">Identity ticket user data is expected but not found! Set token to forms authentication cookie user data during logon operation.</exception>
        public static AuthenticatedUserInfo GetAuthUser()
        {
            try
            {
                FormsIdentity identity = System.Web.HttpContext.Current.User.Identity as FormsIdentity;
                if (identity == null)
                    throw new Exception("Authentication required!");

                FormsAuthenticationTicket ticket = identity.Ticket;
                if (String.IsNullOrEmpty(ticket.UserData))
                    throw new NullReferenceException("Identity ticket user data is expected but not found! Set token to forms authentication cookie user data during logon operation.");

                var tokenStr = ticket.UserData;
                var token = new JavaScriptSerializer().Deserialize<AuthenticatedUserInfo>(tokenStr);
                return token;
            }
            catch (Exception ex)
            {
                return new NullAuthenticatedUserInfo(ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="principal"></param>
        public static void SetPrincipal(AuthenticatedUserInfoPrincipal principal)
        {
            Thread.CurrentPrincipal = principal;
            if (HttpContext.Current != null)
            {
                principal.SetIdentity(HttpContext.Current.User.Identity);
                HttpContext.Current.User = principal;
            }
        }

        /// <summary>
        /// Destroys this instance.
        /// </summary>
        public static void Destroy()
        {
            Thread.CurrentPrincipal = null;

            if (HttpContext.Current != null)
            {
                HttpContext.Current.Session.Abandon();
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.Cookies.Clear();

                HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                HttpContext.Current.Response.Cache.SetExpires(DateTime.Now);
                HttpContext.Current.Response.Cache.SetNoServerCaching();
                HttpContext.Current.Response.Cache.SetNoStore();
            }
        }

        /// <summary>
        /// Sets the expired cookie.
        /// </summary>
        public static void SetExpiredCookie()
        {
            HttpCookie cookie = CookieManager.Create(new AuthenticatedUserInfo() { });
            cookie.Expires = DateTime.Now.AddDays(-1);
            HttpContext.Current.Response.SetCookie(cookie);
        }

        /// <summary>
        /// Call in Global.asax to set Principal to Thread.CurrentPrincipal and HttpContext.Current.User if exist
        /// </summary>
        public static void CallPostAuthenticateRequest()
        {
            var authUser = GetAuthUser();
            if (authUser != null && !(authUser is NullAuthenticatedUserInfo))
            {
                var principal = new AuthenticatedUserInfoPrincipal(authUser);
                SetPrincipal(principal);
            }
        }
    }
}

