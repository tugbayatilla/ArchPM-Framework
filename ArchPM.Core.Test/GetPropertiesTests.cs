using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using ArchPM.Core.Extensions;
using System.Collections;
using System.Linq;

namespace ArchPM.Core.Tests
{
    [TestClass]
    public class GetPropertiesTests
    {
        [TestMethod]
        public void GetPropertiesWhenValidObjectGivenThenReturnsValid()
        {
            var type = typeof(GetPropertiesTestClass);

            var properties = type.CollectProperties();

            Assert.AreEqual("IntValue", properties.First().Name);
            Assert.AreEqual(typeof(TestAttribute), properties.First().Attributes.First().GetType());
        }

        [TestMethod]
        public void GetPropertiesWhenInt32GivenThenReturnsValid()
        {
            var type = typeof(Int32);

            var properties = type.CollectProperties();
            Assert.IsTrue(properties.Count() == 0);
        }

    }

    class GetPropertiesTestClass
    {
        [TestAttribute]
        public int IntValue { get; set; }
    }

    class TestAttribute : Attribute
    { }

}
