using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using ArchPM.Core.Extensions;
using System.Collections;

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

        [TestMethod]
        public void IsListWhenValidCollectionsGivenThenReturnsValidResponse()
        {
            Assert.IsTrue(typeof(ArrayList).IsList(), $"{nameof(ArrayList)} is a list but not return as a list. Failed!");
            Assert.IsTrue(typeof(List<Object>).IsList(), $"{nameof(List<Object>)} is a list but not return as a list. Failed!");
            Assert.IsTrue(typeof(LinkedList<Object>).IsList(), $"{nameof(LinkedList<Object>)} is a list but not return as a list. Failed!");
            Assert.IsTrue(typeof(Queue).IsList(), $"{nameof(Queue)} is a list but not return as a list. Failed!");
            Assert.IsTrue(typeof(Queue<Object>).IsList(), $"{nameof(Queue<Object>)} is a list but not return as a list. Failed!");
            Assert.IsTrue(typeof(Stack).IsList(), $"{nameof(Stack)} is a list but not return as a list. Failed!");
            Assert.IsTrue(typeof(Stack<Object>).IsList(), $"{nameof(Stack<Object>)} is a list but not return as a list. Failed!");
            Assert.IsTrue(typeof(IEnumerable).IsList(), $"{nameof(IEnumerable)} is a list but not return as a list. Failed!");
            Assert.IsTrue(typeof(IEnumerable<Object>).IsList(), $"{nameof(IEnumerable<Object>)} is a list but not return as a list. Failed!");
            Assert.IsTrue(typeof(ICollection).IsList(), $"{nameof(ICollection)} is a list but not return as a list. Failed!");
            Assert.IsTrue(typeof(ICollection<Object>).IsList(), $"{nameof(ICollection<Object>)} is a list but not return as a list. Failed!");
            Assert.IsTrue(typeof(IList).IsList(), $"{nameof(IList)} is a list but not return as a list. Failed!");
            Assert.IsTrue(typeof(IList<Object>).IsList(), $"{nameof(IList<Object>)} is a list but not return as a list. Failed!");
            Assert.IsTrue(typeof(IReadOnlyList<Object>).IsList(), $"{nameof(IReadOnlyList<Object>)} is a list but not return as a list. Failed!");
            Assert.IsTrue(typeof(IReadOnlyCollection<Object>).IsList(), $"{nameof(IReadOnlyCollection<Object>)} is a list but not return as a list. Failed!");
        }

        [TestMethod]
        public void IsDotnetPrimitiveValid()
        {
            Assert.IsTrue(typeof(String).IsDotNetPirimitive(), $"{nameof(String)} is dotnet primitive!");
            Assert.IsTrue(typeof(Char).IsDotNetPirimitive(), $"{nameof(Char)} is dotnet primitive!");
            Assert.IsTrue(typeof(Byte).IsDotNetPirimitive(), $"{nameof(Byte)} is dotnet primitive!");
            Assert.IsTrue(typeof(Int32).IsDotNetPirimitive(), $"{nameof(Int32)} is dotnet primitive!");
            Assert.IsTrue(typeof(Int64).IsDotNetPirimitive(), $"{nameof(Int64)} is dotnet primitive!");
            Assert.IsTrue(typeof(Int16).IsDotNetPirimitive(), $"{nameof(Int16)} is dotnet primitive!");
            Assert.IsTrue(typeof(float).IsDotNetPirimitive(), $"float is dotnet primitive!");
            Assert.IsTrue(typeof(long).IsDotNetPirimitive(), $"long is dotnet primitive!");
            Assert.IsTrue(typeof(short).IsDotNetPirimitive(), $"short is dotnet primitive!");
            Assert.IsTrue(typeof(Double).IsDotNetPirimitive(), $"{nameof(Double)} is dotnet primitive!");
            Assert.IsTrue(typeof(Decimal).IsDotNetPirimitive(), $"{nameof(Decimal)} is dotnet primitive!");
            Assert.IsTrue(typeof(DateTime).IsDotNetPirimitive(), $"{nameof(DateTime)} is dotnet primitive!");
            Assert.IsTrue(typeof(Boolean).IsDotNetPirimitive(), $"{nameof(Boolean)} is dotnet primitive!");
            Assert.IsTrue(typeof(Guid).IsDotNetPirimitive(), $"{nameof(Guid)} is dotnet primitive!");
            Assert.IsTrue(typeof(Enum).IsDotNetPirimitive(), $"{nameof(Enum)} is dotnet primitive!");
            Assert.IsTrue(typeof(uint).IsDotNetPirimitive(), $"uint is dotnet primitive!");
            Assert.IsTrue(typeof(ulong).IsDotNetPirimitive(), $"ulong is dotnet primitive!");
            Assert.IsTrue(typeof(ushort).IsDotNetPirimitive(), $"ushort is dotnet primitive!");
            Assert.IsTrue(typeof(sbyte).IsDotNetPirimitive(), $"sbyte is dotnet primitive!");

            Assert.IsTrue(typeof(Char?).IsDotNetPirimitive(), $"{nameof(Char)}? is dotnet primitive!");
            Assert.IsTrue(typeof(Byte?).IsDotNetPirimitive(), $"{nameof(Byte)}? is dotnet primitive!");
            Assert.IsTrue(typeof(Int32?).IsDotNetPirimitive(), $"{nameof(Int32)}? is dotnet primitive!");
            Assert.IsTrue(typeof(Int64?).IsDotNetPirimitive(), $"{nameof(Int64)}? is dotnet primitive!");
            Assert.IsTrue(typeof(Int16?).IsDotNetPirimitive(), $"{nameof(Int16)}? is dotnet primitive!");
            Assert.IsTrue(typeof(float?).IsDotNetPirimitive(), $"float? is dotnet primitive!");
            Assert.IsTrue(typeof(long?).IsDotNetPirimitive(), $"long? is dotnet primitive!");
            Assert.IsTrue(typeof(short?).IsDotNetPirimitive(), $"short? is dotnet primitive!");
            Assert.IsTrue(typeof(Double?).IsDotNetPirimitive(), $"{nameof(Double)}? is dotnet primitive!");
            Assert.IsTrue(typeof(Decimal?).IsDotNetPirimitive(), $"{nameof(Decimal)}? is dotnet primitive!");
            Assert.IsTrue(typeof(DateTime?).IsDotNetPirimitive(), $"{nameof(DateTime)}? is dotnet primitive!");
            Assert.IsTrue(typeof(Boolean?).IsDotNetPirimitive(), $"{nameof(Boolean)}? is dotnet primitive!");
            Assert.IsTrue(typeof(Guid?).IsDotNetPirimitive(), $"{nameof(Guid)}? is dotnet primitive!");
            Assert.IsTrue(typeof(uint?).IsDotNetPirimitive(), $"uint? is dotnet primitive!");
            Assert.IsTrue(typeof(ulong?).IsDotNetPirimitive(), $"ulong? is dotnet primitive!");
            Assert.IsTrue(typeof(ushort?).IsDotNetPirimitive(), $"ushort? is dotnet primitive!");
            Assert.IsTrue(typeof(sbyte?).IsDotNetPirimitive(), $"sbyte? is dotnet primitive!");



        }

        [TestMethod]
        public void IsDotnetPrimitiveWhenClassFailed()
        {

            Assert.IsFalse(typeof(AllLowerWithUnderscoreClass).IsDotNetPirimitive(), $"{nameof(AllLowerWithUnderscoreClass)} is not dotnet primitive!");


        }
    }
}
