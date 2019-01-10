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
    public class DatabaseProviderTests
    {
        [TestMethod]
        public void WhenConnectionStringInValidThenThrowException()
        {
            var request = new ComplexObjectRequest
            {
                Id = 1
            };

            var engine = new ApiQueryEngine<ComplexObjectRequest, ComplexObjectResponse>(
                new OracleApiQueryAdaptor("asd"));

            var responseTask = engine.Execute(request);
            var response = responseTask.GetAwaiter().GetResult();
            Assert.AreEqual("900", response.Code, response.Message);

            Assert.IsNull(response.Data, "response.Data is null");
            Assert.IsTrue(response.Errors.Count == 1);
            Assert.AreEqual("Database connection string is not valid!", response.Message);

        }
    }
}
