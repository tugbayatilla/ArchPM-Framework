using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchPM.Core.IO
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Diagnostics.TraceListener" />
    public class LogFileTraceListener : TraceListener
    {
        internal readonly LogToFileManager logToFileManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="LogFileTraceListener"/> class.
        /// </summary>
        public LogFileTraceListener()
        {
            logToFileManager = new LogToFileManager();
            
            Trace.Listeners.Add(this);
        }

        /// <summary>
        /// When overridden in a derived class, writes the specified message to the listener you create in the derived class.
        /// </summary>
        /// <param name="message">A message to write.</param>
        public override async void Write(string message)
        {
            await logToFileManager.AppendToFile(message, false);
        }

        /// <summary>
        /// When overridden in a derived class, writes a message to the listener you create in the derived class, followed by a line terminator.
        /// </summary>
        /// <param name="message">A message to write.</param>
        public override async void WriteLine(string message)
        {
            await logToFileManager.AppendToFile(message);
        }
    }
}
