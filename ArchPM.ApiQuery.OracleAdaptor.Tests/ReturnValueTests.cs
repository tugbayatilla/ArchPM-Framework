using System;
using System.Collections.Generic;
using System.Linq;
using ArchPM.ApiQuery.OracleAdaptor.Tests.Model;
using ArchPM.Core.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ArchPM.ApiQuery.OracleAdaptor.Tests
{
    [TestClass]
    public class ReturnValueTests
    {
        [TestMethod]
        public void InsertSample1RequestWhenRequestValidReturnValidResponse()
        {
            var request = new InsertSample1Request
            {
                NumberValueNotNull = 10,
                NumberValueNull = 10,
                StringValueNotNull = "SingleListWhenRequestValidReturnValidResponse",
                StringValueNull = null
            };

            var engine = new ApiQueryEngine<InsertSample1Request, Int32>(
                new OracleApiQueryAdaptor("OracleConnection"));

            var responseTask = engine.Execute(request);
            var response = responseTask.GetAwaiter().GetResult();
            Assert.AreEqual("200", response.Code, response.Message);
            Assert.IsNotNull(response.Data, response.Message);
           
        }


        

    }
}