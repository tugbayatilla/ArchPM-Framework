using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ArchPM.Core.Tests
{
    /// <summary>
    /// Summary description for ExtensionMethodsTests
    /// </summary>
    [TestClass]
    public class ExtensionMethodsTests
    {
        [TestMethod]
        public void ThrowExceptionIf_WhenObjectIsNotNull_PredicateIsNotNull_ExceptionIsNotNull_ThrowsNothing()
        {
            Object obj = new object();
            obj.ThrowExceptionIf(p => p == null, new Exception("Object is null here"));
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void ThrowExceptionIf_WhenObjectIsNull_PredicateIsNotNull_ExceptionIsNotNull_ThrowsNullReferenceException()
        {
            Object obj = null;
            obj.ThrowExceptionIf(p => p == null, new NullReferenceException("Object is null here"));
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "predicate is null!")]
        public void ThrowExceptionIf_WhenObjectIsNull_PredicateIsNull_ExceptionIsNotNull_ThrowsException()
        {
            Object obj = null;
            obj.ThrowExceptionIf(predicate:null, exception: new NullReferenceException("Object is null here"));
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "An object 'obj' instance can't be null")]
        public void ThrowExceptionIf_WhenObjectIsNull_PredicateIsNotNull_ExceptionIsNull_ThrowsException()
        {
            Object obj = null;
            obj.ThrowExceptionIf(p => p == null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException), "ArgumentOutOfRangeException is throwing here")]
        public void ThrowExceptionIfNull_WhenObjectIsNull_ExceptionIsArgumentOutOfRangeException_GivenMessage_ThrowsArgumentOutOfRangeException()
        {
            Object obj = null;
            obj.ThrowExceptionIfNull<ArgumentOutOfRangeException>("ArgumentOutOfRangeException is throwing here");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException), "An object 'obj' instance can't be null")]
        public void ThrowExceptionIfNull_WhenObjectIsNull_ExceptionIsArgumentOutOfRangeException_NoGivenMessage_ThrowsArgumentOutOfRangeException()
        {
            Object obj = null;
            obj.ThrowExceptionIfNull<ArgumentOutOfRangeException>();
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "sample given message")]
        public void ThrowExceptionIfNull_WhenObjectIsNull_GivenMessage_ThrowsException()
        {
            Object obj = null;
            obj.ThrowExceptionIfNull("sample given message");
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "")]
        public void ThrowExceptionIfNull_WhenObjectIsNull_EmptyStringGivenMessage_ThrowsException()
        {
            Object obj = null;
            obj.ThrowExceptionIfNull("");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "sample")]
        public void ThrowExceptionIfNull_WhenObjectIsNull_GivenException_ThrowsException()
        {
            Object obj = null;
            obj.ThrowExceptionIfNull(new ArgumentNullException("sample"));
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "An object 'obj' instance can't be null")]
        public void ThrowExceptionIfNull_WhenObjectIsNull_GivenExceptionAsNull_ThrowsException()
        {
            Object obj = null;
            obj.ThrowExceptionIfNull(exception: null);
        }
    }
}
