using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ArchPM.ApiQuery.OracleAdaptor.Tests.Model;

namespace ArchPM.ApiQuery.OracleAdaptor.Tests
{
    /// <summary>
    /// Summary description for MultiListTests
    /// </summary>
    [TestClass]
    public class MultiListTests
    {
        [TestMethod]
        public void MultiListRequestWhenValidRequestThenReturnsValidResponse()
        {
            var request = new MultiListRequest
            {
                Id = 1
            };

            var engine = new ApiQueryEngine<MultiListRequest, MultiListResponse>(
                new OracleApiQueryAdaptor("OracleConnection"));

            var responseTask = engine.Execute(request);
            var response = responseTask.GetAwaiter().GetResult();
            Assert.AreEqual("200", response.Code, response.Message);
            Assert.IsNotNull(response.Data, response.Message);
            Assert.IsNotNull(response.Data.List1, response.Message);
            Assert.IsNotNull(response.Data.List2, response.Message);
            Assert.IsTrue(response.Data.List1.Count > 0, response.Message);
            Assert.IsTrue(response.Data.List2.Count > 0, response.Message);
        }
    }
}
