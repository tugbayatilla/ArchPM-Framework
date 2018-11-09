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

        [TestMethod]
        public void TryToConvertWhenIntegerAsStringThenReturnsValid()
        {
            String value = "3";

            var result = value.TryToConvert(typeof(int), 1);
            Assert.AreEqual(3, result);
        }

        [TestMethod]
        public void TryToConvertWhenInValidIntegerAsStringThenReturnsDefaultValue()
        {
            String value = "-";

            var result = value.TryToConvert(typeof(int), 1);
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void TryToConvertWhenInValidIntegerAsStringWithoutDefaultValueThenReturnsDefaultValue()
        {
            String value = "-";

            var result = value.TryToConvert(typeof(int));
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void TryToConvertWhenInValidDatetimeAsStringWithoutDefaultValueThenReturnsDefaultValue()
        {
            String value = "-";

            var result = value.TryToConvert(typeof(DateTime));
            Assert.AreEqual(default(DateTime), result);
        }


    }
}
