﻿using System;
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
        public MultiListTests()
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