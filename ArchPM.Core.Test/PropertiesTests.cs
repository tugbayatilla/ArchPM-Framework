using System;
using System.Linq;
using ArchPM.Core.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ArchPM.Core.Tests
{
    [TestClass]
    public class PropertiesTests
    {
        [TestMethod]
        public void PropertiesWhenAttributeDefinedAttributesPropertyWorks()
        {
            AttributedClass1 class1 = new AttributedClass1();
            var attributes = class1.Properties().SelectMany(p => p.Attributes);

            Assert.AreEqual(3, attributes.Count());
        }

        [TestMethod]
        public void PropertiesWhenAttributeDefinedOnPropertiesOndefinedAttributesCanBeCollected()
        {
            AttributedClass1 class1 = new AttributedClass1();
            var properties = class1.Properties(p => p.Attributes.Any(x => x.GetType() == typeof(TestAttribute1))).ToList();

            properties.ForEach(p=> {
                Console.WriteLine(p.Name);
            });

            Assert.AreEqual(2, properties.Count());

            Assert.AreEqual("AttributedProperty1", properties[0].Name);
            Assert.AreEqual("AttributedProperty2", properties[1].Name);
        }

        [TestMethod]
        public void PropertiesWhenSystemAttributeDefinedOnPropertiesReturnsSystemProperty()
        {
            AttributedClass1 class1 = new AttributedClass1();
            var properties = class1.Properties(p => p.Attributes.Any(x => x.GetType() == typeof(ObsoleteAttribute))).ToList();

            properties.ForEach(p => {
                Console.WriteLine(p.Name);
            });

            Assert.AreEqual(1, properties.Count());
            Assert.AreEqual("SystemAttributedProperty1", properties[0].Name);
        }

        [TestMethod]
        public void XXX()
        {
            ParentClass parent = new ParentClass();
            parent.Child1 = new ChildClass1();
            parent.Child1s = new System.Collections.Generic.List<ChildClass1>();

            var properties = parent.PropertiesAll().ToList();

            properties.ForEach(p => {
                Console.WriteLine(p.Name);
            });
        }


    }
}
