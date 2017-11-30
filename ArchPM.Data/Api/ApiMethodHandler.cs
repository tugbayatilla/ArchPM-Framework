using ArchPM.Core.Exceptions;
using ArchPM.Core.Logging.BasicLogging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchPM.Data.Api
{
    public class ApiMethodHandler<T>
    {
        Stopwatch sw;
        Action<ApiResponse<T>, Exception> catchAction;
        ApiResponse<T> _result = null;
        IBasicLog basicLog;
        private ApiMethodHandler()
        {
            this.basicLog = new NullBasicLog();
        }

        public static ApiMethodHandler<U> Create<U>()
        {
            return new ApiMethodHandler<U>();
        }
       
        public ApiResponse<T> Result
        {
            get
            {
                _result.ET = sw.ElapsedMilliseconds;
                sw.Stop();

                return _result;
            }
        }

        public ApiMethodHandler<T> Try(Func<ApiResponse<T>> tryFunc)
        {
            ApiResponse<T> result = null;
            try
            {
                sw = Stopwatch.StartNew();
                result = tryFunc();
            }

            catch (ValidationException ex)
            {
                result = ApiResponse<T>.CreateException(ex);
            }
            catch (Exception ex)
            {
                result = ApiResponse<T>.CreateException(ex);
                this.basicLog.Log(ex);

                if (catchAction != null)
                    catchAction(result, ex);
            }

            return this;
        }

        public ApiMethodHandler<T> Catch(Action<ApiResponse<T>, Exception> catchAction)
        {
            this.catchAction = catchAction;
            return this;
        }

        public ApiMethodHandler<T> SetBasicLog(IBasicLog basicLog)
        {
            this.basicLog = basicLog;
            return this;
        }

    }
}
