using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using ArchPM.Core.Extensions;
using ArchPM.Core.Tests.Domain;
using System.Linq;
using ArchPM.Core.Tests.ApiHelpTestObjects;

namespace ArchPM.Core.Tests
{
    [TestClass]
    public class ApiHelpTests
    {

        [TestMethod]
        public void Class_WithApiHelp_Method_WithoutApiHelp_WithoutRequest_WithResponseAsTask_ApiResponse_Object()
        {
            var response = new Api.ApiHelpResponse(typeof(ApiResponseSamples));

            var method = response.Actions.FirstOrDefault(p=>p.Name == "Method_WithoutApiHelp_WithoutRequest_WithResponseAsTask_ApiResponse_Object");

            Assert.AreEqual("ApiResponse<ApiHelpResponse>", method.OutputParameter.Type);
        }

    }
}
