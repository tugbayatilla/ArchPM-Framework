using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using ArchPM.Core.Extensions;

namespace ArchPM.Core.Tests
{
    [TestClass]
    public class ExtensionTests
    {
        [TestMethod]
        public void CalculateProfit_WhenValidReturnValid()
        {
            Decimal capital = 200M;
            Decimal profit = capital.CalculateProfit(10);

            Assert.AreEqual(220, profit);

        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "test")]
        public void ThrowExceptionIfNull_CheckCallerName()
        {
            Object test = null;
            test.ThrowExceptionIfNull();
        }


    }
}
