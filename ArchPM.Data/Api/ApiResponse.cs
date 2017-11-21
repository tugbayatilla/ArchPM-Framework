using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ArchPM.Core;
using ArchPM.Core.Extensions;

namespace ArchPM.Data.Api
{
    /// <summary>
    /// To be able to use same format sending json response to client. 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="ArchPM.Data.Api.IApiResponse" />
    public class ApiResponse<T> : IApiResponse
    {
        /// <summary>
        /// Gets or sets a value the requested operation whether is operated correctly or not. 
        /// this is not HttpResponse result.
        /// </summary>
        /// <value>
        ///   <c>true</c> if result; otherwise, <c>false</c>.
        /// </value>
        public Boolean Result { get; set; }
        /// <summary>
        /// Gets or sets the application specific code.
        /// </summary>
        /// <value>
        /// The code.
        /// </value>
        public String Code { get; set; }
        /// <summary>
        /// Gets or sets the errors.
        /// </summary>
        /// <value>
        /// The errors.
        /// </value>
        public List<ApiError> Errors { get; set; }
        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value>
        /// The data.
        /// </value>
        public T Data { get; set; }
        /// <summary>
        /// Gets or sets the et.
        /// </summary>
        /// <value>
        /// The Execution Time
        /// </value>
        public Double ET { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiResponse{T}"/> class.
        /// </summary>
        public ApiResponse()
        {
            this.Errors = new List<ApiError>();
        }

        /// <summary>
        /// Creates the exception.
        /// </summary>
        /// <param name="ex">The ex.</param>
        /// <returns></returns>
        public static ApiResponse<T> CreateException(Exception ex)
        {
            ApiResponse<T> obj = new ApiResponse<T>();

            obj.Result = false;
            obj.Code = ApiResponseCodes.ERROR;
            obj.Data = default(T);

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
        public static ApiResponse<T> CreateSuccessResponse(T data)
        {
            ApiResponse<T> obj = new ApiResponse<T>
            {
                Result = true,
                Code = ApiResponseCodes.OK,
                Data = data,
                Errors = null
            };

            return obj;
        }

    }
}
