using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ArchPM.ApiQuery.Tests.Model;

namespace ArchPM.ApiQuery.Tests
{
    /// <summary>
    /// Summary description for MultiListTests
    /// </summary>
    [TestClass]
    public class ComplexObjectTests
    {
        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void ComplexObjectRequestWhenValidRequestThenReturnsValidResponse()
        {
            var request = new ComplexObjectRequest
            {
                Id = 1
            };

            var engine = new ApiQueryEngine<ComplexObjectRequest, ComplexObjectResponse>(
                new OracleApiQueryProvider("OracleConnection"));

            var responseTask = engine.Execute(request);
            var response = responseTask.GetAwaiter().GetResult();
            Assert.AreEqual("200", response.Code, response.Message);
            Assert.IsNotNull(response.Data, "response.Data is null");
            Assert.IsNotNull(response.Data.Complex, "response.Data.Complex is null");
            Assert.IsNotNull(response.Data.List2, "response.Data.List2 is null");
            Assert.IsTrue(response.Data.Complex.Sample3Id > 0, response.Message);
            Assert.IsTrue(response.Data.List2.Count > 0, response.Message);
        }
    }
}
