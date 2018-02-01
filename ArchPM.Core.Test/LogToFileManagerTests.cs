using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using ArchPM.Core.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ArchPM.Core.Tests
{
    [TestClass]
    public class LogToFileManagerTests
    {
        [TestMethod]
        public void LogFileTraceListener_WhenInitializedthenListenersHaveThisInstance()
        {
            LogFileTraceListener tracer = new LogFileTraceListener();
            Assert.IsTrue(Trace.Listeners.Contains(tracer));
        }

        //[TestMethod]
        //public void Write_WhenMessageIsValidThenLogWritesToFile()
        //{
        //    LogFileTraceListener tracer = new LogFileTraceListener();
        //    DateTime now = DateTime.Now;
        //    var msg = $"TIME:{now.ToLongTimeString()}_{Guid.NewGuid()}";
        //    tracer.WriteLine(msg);

        //    var filePath = Path.Combine(tracer.logToFileManager.LogDirectoryPath, tracer.logToFileManager.LogFileNameWithExtension);
        //    var fileStream = IO.Utils.WaitFileTillReadyToRead(filePath);
        //    Task.WaitAll(fileStream);
        //    using (var sr = new StreamReader(fileStream.Result))
        //    {
        //        var text = sr.ReadToEnd();
        //        Assert.IsTrue(text.Contains(msg));
        //    }

        //}
    }
}
