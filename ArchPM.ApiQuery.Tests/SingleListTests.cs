using System;
using System.Collections.Generic;
using System.Linq;
using ArchPM.ApiQuery.Tests.Model;
using ArchPM.Core.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ArchPM.ApiQuery.Tests
{
    [TestClass]
    public class SingleListTests
    {
        [TestMethod]
        public void SingleListWhenRequestValidReturnValidResponse()
        {
            var request = new SingleListRequest
            {
                Id = 1
            };

            var engine = new ApiQueryEngine<SingleListRequest, List<SingleListResponse>>(
                new OracleApiQueryProvider("OracleConnection"));

            var responseTask = engine.Execute(request);
            var response = responseTask.GetAwaiter().GetResult();
            Assert.AreEqual("200", response.Code, response.Message);
            Assert.IsNotNull(response.Data, response.Message);
        }

        [TestMethod]
        public void SingleListWhenRequestUndefinedOutputAttributeOnListClassThenReturnsInvalidResponse()
        {
            var request = new SingleListRequest
            {
                Id = 1
            };

            var engine = new ApiQueryEngine<SingleListRequest, List<SingleListResponseNoAttribute>>(
                new OracleApiQueryProvider("OracleConnection"));

            var responseTask = engine.Execute(request);
            var response = responseTask.GetAwaiter().GetResult();
            Assert.AreEqual("900", response.Code, response.Message);
            Assert.AreEqual($"Failed at CreateCommandParameters!\r\n{ nameof(OutputApiQueryFieldAttribute)} must be used on {nameof(SingleListResponseNoAttribute)}!", response.Message);
            Assert.IsNull(response.Data, response.Message);
        }


    }
}