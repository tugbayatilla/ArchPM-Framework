using ArchPM.Core.Exceptions;
using ArchPM.Data.Api;
using ArchPM.Web.Core.Domain;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ArchPM.Web.Core
{
    public class BaseController : Controller
    {
        protected virtual new AuthenticatedUserInfo User
        {
            get { return HttpContext.User as AuthenticatedUserInfo; }
        }

        protected JsonResult TryCatch<T>(Func<ApiResponse<T>> action)
        {
            ApiResponse<T> result = null;
            Stopwatch sw = Stopwatch.StartNew();
            try
            {
                result = action();
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

    }
}
