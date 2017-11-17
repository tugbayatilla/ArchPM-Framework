using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ArchPM.Core.Extensions.Advanced;
using ArchPM.Core.Extensions;

namespace ArchPM.Core.Tests
{
    [TestClass]
    public class ReflectionExtensionTests
    {
        public void FillDummyData<T>(T entity) where T : class, new()
        {
            if (entity == null)
                return;

            entity.Properties().ForEach(p =>
            {
                Object value = null;
                switch (p.ValueType)
                {
                    case "String":
                        value = "test";
                        break;
                    case "Int32":
                        value = 99;
                        break;
                    case "Int64":
                        value = Convert.ToInt64(99);
                        break;
                    case "Int16":
                        value = Convert.ToInt16(99);
                        break;
                    case "Float":
                        value = Convert.ToInt16(99);
                        break;
                    case "Decimal":
                        value = Convert.ToDecimal(99);
                        break;
                    case "DateTime":
                        value = new DateTime(2000, 1, 1);
                        break;
                    case "Boolean":
                        value = true;
                        break;
                    case "Guid":
                        value = Guid.Empty;
                        break;
                    default:
                        break;
                }

                if (p.IsEnum)
                {
                    var values = Enum.GetValues(p.ValueTypeOf);
                    if (values.Length > 0)
                        value = values.GetValue(0);
                }

                if (value != null)
                {
                    var prop = entity.GetType().GetProperty(p.Name);
                    if (prop.CanWrite)
                        prop.SetValue(entity, value);
                }

            });
        }


        [TestMethod]
        public void FillRandomDataGivenEntityWhenInistantiateThenReturnsEntityWithFilledProperties()
        {
            Person p = new Person();
            FillDummyData(p);

            Assert.AreEqual("test", p.Name, "Name");
            Assert.AreEqual("test", p.Name2, "Name2");
            Assert.AreEqual(new DateTime(2000,1,1), p.Birth, "Birth");
            Assert.AreEqual(new DateTime(2000,1,1), p.Birth2, "Birth2");
            Assert.AreEqual(Fears.Dark, p.Fear, "Fear");
            Assert.AreEqual(Fears.Dark, p.Fear2, "Fear2");
            Assert.AreEqual(Genders.Male, p.Gender, "Gender");
            Assert.AreEqual(Genders.Male, p.Gender2, "Gender2");
            Assert.AreEqual(99, p.Height, "Height");
            Assert.AreEqual(99, p.Height2, "Height2");
            Assert.AreEqual(99, p.Id, "Id");
            Assert.AreEqual(99, p.Id2, "Id2");
            Assert.AreEqual(true, p.IsFriendly, "IsFriendly");
            Assert.AreEqual(true, p.IsFriendly2, "IsFriendly2");
            Assert.AreEqual("test", p.OnlyRead, "OnlyRead");
        }

    }
}
