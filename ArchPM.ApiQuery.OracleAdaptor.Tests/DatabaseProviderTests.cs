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
        public DatabaseProviderTests()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

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
