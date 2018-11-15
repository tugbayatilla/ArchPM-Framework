using ArchPM.Core.Exceptions;
using ArchPM.Core.Extensions;
using System;
using System.Collections.Generic;

namespace ArchPM.Core.Api
{
    /// <summary>
    /// To be able to use same format sending json response to client. 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="ArchPM.Core.Api.IApiResponse" />
    public class ApiResponse<T> : IApiResponse<T>
    {
        /// <summary>
        /// Gets or sets a value the requested operation whether is operated correctly or not. 
        /// this is not HttpResponse result.
        /// </summary>
        /// <value>
        ///   <c>true</c> if result; otherwise, <c>false</c>.
        /// </value>
        public virtual Boolean Result { get; set; }
        /// <summary>
        /// Gets or sets the application specific code.
        /// </summary>
        /// <value>
        /// The code.
        /// </value>
        public virtual String Code { get; set; }
        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>
        /// The code.
        /// </value>
        public virtual String Message { get; set; }
        /// <summary>
        /// Gets the source.
        /// </summary>
        /// <value>
        /// Default is Core
        /// </value>
        public virtual String Source => "Core";
        /// <summary>
        /// Gets or sets the errors.
        /// </summary>
        /// <value>
        /// The errors.
        /// </value>
        public virtual List<ApiError> Errors { get; set; }
        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value>
        /// The data.
        /// </value>
        public virtual T Data { get; set; }
        /// <summary>
        /// Gets or sets the et.
        /// </summary>
        /// <value>
        /// The Execution Time
        /// </value>
        public virtual Int64 ET { get; set; }

        /// <summary>
        /// Gets or sets the requested URL.
        /// </summary>
        /// <value>
        /// The requested URL.
        /// </value>
        public String RequestedUrl { get; set; }

        /// <summary>
        /// Gets or sets the try count.
        /// </summary>
        /// <value>
        /// The try count.
        /// </value>
        public Int32 TryCount { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiResponse{T}"/> class.
        /// </summary>
        public ApiResponse()
        {
            this.Errors = new List<ApiError>();
            this.TryCount = 0;
        }

        /// <summary>
        /// Creates the exception.
        /// </summary>
        /// <param name="ex">The ex.</param>
        /// <returns></returns>
        [Obsolete("this will be removed version 0.4.0. Use CreateFail instead!")]
        public static IApiResponse<T> CreateException(Exception ex)
        {
            IApiResponse<T> obj = new ApiResponse<T>
            {
                Result = false,
                Code = SetErrorCodeBasedOnException(ex),
                Data = default(T),
                Message = ex.GetAllMessages(false)
            };

            ex.GetAllExceptions().ForEach(p =>
            {
                obj.Errors.Add(new ApiError() { Message = ex.Message });
            });

            return obj;
        }
        
       /// <summary>
        /// Creates the success response.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        [Obsolete("this will be removed version 0.4.0. Use CreateSuccess instead!")]
        public static IApiResponse<T> CreateSuccessResponse(T data)
        {
            IApiResponse<T> obj = new ApiResponse<T>
            {
                Result = true,
                Code = ApiResponseCodes.OK,
                Data = data,
                Errors = null
            };

            return obj;
        }

        /// <summary>
        /// Creates the exception.
        /// </summary>
        /// <param name="ex">The ex.</param>
        /// <param name="getDetailedErrors">if set to <c>true</c> [get seperate errors].</param>
        /// <returns></returns>
        public static ApiResponse<T> CreateFail(Exception ex, Boolean getDetailedErrors = true)
        {
            var obj = new ApiResponse<T>
            {
                Result = false,
                Code = SetErrorCodeBasedOnException(ex),
                Data = default(T),
                Message = ex.GetAllMessages(false)
            };

            if (getDetailedErrors)
            {
                ex.GetAllExceptions().ForEach(p =>
                {
                    obj.Errors.Add(new ApiError() { Message = ex.Message, DetailedMessage = ex.StackTrace });
                });
            }

            return obj;
        }

        /// <summary>
        /// Creates the success.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        public static ApiResponse<T> CreateSuccess(T data)
        {
            var obj = new ApiResponse<T>
            {
                Result = true,
                Code = ApiResponseCodes.OK,
                Data = data,
                Errors = null
            };

            return obj;
        }

        /// <summary>
        /// Sets the error code based on exception.
        /// </summary>
        /// <param name="ex">The ex.</param>
        /// <returns></returns>
        protected internal static String SetErrorCodeBasedOnException(Exception ex)
        {
            if (ex is System.Security.Authentication.AuthenticationException || ex is Core.Exceptions.AuthenticationException)
            {
                return ApiResponseCodes.AUTHENTICATION_FAILED;
            }
            else if (ex is AuthorizationException)
            {
                return ApiResponseCodes.AUTHORIZATION_FAILED;
            }
            else if (ex is ValidationException)
            {
                return ApiResponseCodes.VALIDATION_ERROR;
            }
            else
            {
                return ApiResponseCodes.ERROR;
            }
        }


    }
}
