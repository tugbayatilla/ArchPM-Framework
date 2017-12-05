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
        public void GetValueIfItIsCollection_WhenDictionaryIsValidReturnsValid()
        {
            Dictionary<String, Object> containerDictionary = new Dictionary<string, object>();

            List<SmallTable> data = new List<SmallTable>() { new SmallTable
            {
                Id = 1,
                Id2 = 2,
                Id42 = 42,
                Name = "small table",
                Salary = 100m
            }};

            containerDictionary.Add("test", data);

            var result = containerDictionary.GetValueIfItIsCollection<SmallTable>("test");
            Assert.IsTrue(result.Count == 1);
            Assert.IsTrue(result[0] is SmallTable, "result is not SmallTable");

        }
    }
}
