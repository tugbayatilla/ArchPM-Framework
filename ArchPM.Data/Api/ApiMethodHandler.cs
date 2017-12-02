//using ArchPM.Core.Exceptions;
//using ArchPM.Core.Logging.BasicLogging;
//using System;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace ArchPM.Data.Api
//{
//    public class ApiMethodHandler<T>
//    {
//        Stopwatch sw;
//        Action<ApiResponse<T>, Exception> catchAction;
//        ApiResponse<T> _result = null;
//        IBasicLog basicLog;
//        internal ApiMethodHandler()
//        {
//            this.basicLog = new NullBasicLog();
//        }

//        public static ApiMethodHandler<T> Create()
//        {
//            return new ApiMethodHandler<T>();
//        }

//        public ApiResponse<T> Result
//        {
//            get
//            {
//                _result.ET = sw.ElapsedMilliseconds;
//                sw.Stop();

//                return _result;
//            }
//        }

//        public ApiMethodHandler<T> TryCatch(Action<ApiResponse<T>> tryFunc)
//        {
//            _result = new ApiResponse<T>();
//            try
//            {
//                sw = Stopwatch.StartNew();
//                tryFunc(_result);
//            }

//            catch (ValidationException ex)
//            {
//                _result = ApiResponse<T>.CreateException(ex);
//            }
//            catch (Exception ex)
//            {
//                _result = ApiResponse<T>.CreateException(ex);
//                this.basicLog.Log(ex);

//                if (catchAction != null)
//                    catchAction(_result, ex);
//            }

//            return this;
//        }

//        public ApiMethodHandler<T> PostCatch(Action<ApiResponse<T>, Exception> catchAction)
//        {
//            this.catchAction = catchAction;
//            return this;
//        }

//        public ApiMethodHandler<T> SetBasicLog(IBasicLog basicLog)
//        {
//            this.basicLog = basicLog;
//            return this;
//        }

//    }
//}
