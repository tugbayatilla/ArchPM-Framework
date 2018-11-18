using System;
using System.Collections.Generic;
using System.Linq;
using ArchPM.ApiQuery.OracleAdaptor.Tests.Model;
using ArchPM.Core.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ArchPM.ApiQuery.OracleAdaptor.Tests
{
    [TestClass]
    public class GetObjectTests
    {
        [TestMethod]
        public void GetObjectWhenRequestValidReturnValidResponse()
        {
            var request = new GetObjectRequest
            {
                Id = 1
            };

            var engine = new ApiQueryEngine<GetObjectRequest, GetObjectResponse>(
                new OracleApiQueryAdaptor("OracleConnection"));

            var responseTask = engine.Execute(request);
            var response = responseTask.GetAwaiter().GetResult();
            Assert.AreEqual("200", response.Code, response.Message);
            Assert.IsNotNull(response.Data, response.Message);
            Assert.AreEqual(1, response.Data.NumberValueNotNull, response.Message);
            Assert.AreEqual("SAMPLE1_VALUE_NOTNULL_1", response.Data.StringValueNotNull, response.Message);

        }

    }
}