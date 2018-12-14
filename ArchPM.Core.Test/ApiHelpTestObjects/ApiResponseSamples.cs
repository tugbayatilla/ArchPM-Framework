using ArchPM.Core.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchPM.Core.Tests.ApiHelpTestObjects
{
    [ApiHelp]
    public class ApiResponseSamples
    {
        public async Task<ApiResponse<ApiHelpResponse>> Method_WithoutApiHelp_WithoutRequest_WithResponseAsTask_ApiResponse_Object()
        {
            try
            {
                ApiHelpResponse response = new ApiHelpResponse(this.GetType());

                var result = ApiResponse<ApiHelpResponse>.CreateSuccess(response);
                return await Task.FromResult(result);
            }
            catch (Exception ex)
            {
                var result = ApiResponse<ApiHelpResponse>.CreateFail(ex);
                return await Task.FromResult(result);
            }
        }
    }
}
