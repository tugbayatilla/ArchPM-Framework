using ArchPM.Core.Exceptions;
using ArchPM.Core.Session;
using ArchPM.Data.Api;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ArchPM.Web.Core
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Web.Mvc.Controller" />
    public class BaseController : Controller
    {
        /// <summary>
        /// Gets the user security information for the current HTTP request.
        /// </summary>
        protected virtual new AuthenticatedUserInfo User
        {
            get { return HttpContext.User as AuthenticatedUserInfo; }
        }

        /// <summary>
        /// Tries the catch asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action">The action.</param>
        /// <returns></returns>
        protected async Task<JsonResult> TryCatchAsync<T>(Func<Task<ApiResponse<T>>> action)
        {
            ApiResponse<T> result = null;
            Stopwatch sw = Stopwatch.StartNew();
            try
            {
                result = await action();
            }
            catch (ValidationException ex)
            {
                result = ApiResponse<T>.CreateException(ex);
                result.Code = ApiResponseCodes.VALIDATION_ERROR;
            }
            catch (AuthenticationException ex)
            {
                result = ApiResponse<T>.CreateException(ex);
                result.Code = ApiResponseCodes.AUTHENTICATION_FAILED;
            }
            catch (FatalException ex)
            {
                result = ApiResponse<T>.CreateException(ex);
                result.Code = ApiResponseCodes.FATAL_ERROR;

                //mail
            }
            catch (Exception ex)
            {
                result = ApiResponse<T>.CreateException(ex);
                result.Code = ApiResponseCodes.ERROR;
            }
            finally
            {
                result.ET = sw.ElapsedMilliseconds;
                sw.Stop();
            }

            return Json(result);
        }

        protected void SetNoCache()
        {
            Response.Headers.Remove("Cache-Control");
            Response.Headers.Remove("Pragma");
            Response.Headers.Remove("Expires");

            Response.AppendHeader("Cache-Control", "no-cache, no-store, must-revalidate"); // HTTP 1.1.
            Response.AppendHeader("Pragma", "no-cache"); // HTTP 1.0.
            Response.AppendHeader("Expires", "0"); // Proxies.

        }

    }
}
