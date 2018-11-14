using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ArchPM.ApiQuery.Tests.Model;
using ArchPM.Core.Exceptions;

namespace ArchPM.ApiQuery.Tests
{
    /// <summary>
    /// Summary description for MultiListTests
    /// </summary>
    [TestClass]
    public class validationTests
    {
        public validationTests()
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
        [ExpectedException(typeof(ValidationException))]
        public void ValidateWhenStringLenghtGreaterThen50ThenThrowsException()
        {
            ValidationSampleObject obj = new ValidationSampleObject()
            {
                StringPropertyAs50Chars = "123456789012345678901234567890123456789012345678901" //51chars
            };

            obj.Validate();
        }

        [TestMethod]
        public void ValidateWhenStringLenghtLessThen50ThenValid()
        {
            ValidationSampleObject obj = new ValidationSampleObject()
            {
                StringPropertyAs50Chars = "12345678901234567890123456789012345678901234567890",//50chars,
            };

            obj.Validate();
        }


    }
}
