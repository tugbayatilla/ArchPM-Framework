using ArchPM.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchPM.Core.IO
{
    /// <summary>
    /// 
    /// </summary>
    public class LogToFileManager
    {
        /// <summary>
        /// Gets or sets the log directory path.
        /// </summary>
        /// <value>
        /// The log directory path.
        /// </value>
        public String LogDirectoryPath { get; set; }
        /// <summary>
        /// Gets or sets the log file name with extension.
        /// </summary>
        /// <value>
        /// The log file name with extension.
        /// </value>
        public String LogFileNameWithExtension { get; set; }

        /// <summary>
        /// Gets and Sets Waiting milliseconds the file to be ready
        /// </summary>
        /// <value>Default is 10000ms (10s)</value>
        public Int32 WaitMilliseconds { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="LogToFileManager"/> class.
        /// </summary>
        public LogToFileManager()
        {
            this.LogDirectoryPath = Path.Combine(Environment.CurrentDirectory, "Logs");
            this.LogFileNameWithExtension = Utils.GenerateDateTimeBasedFileNameWithExtension();
            this.WaitMilliseconds = 10000;
        }

        /// <summary>
        /// Writes log message to file. Creates the file if not exist
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="isNewLine">if set to <c>true</c> [is new line].</param>
        /// <returns></returns>
        public async Task AppendToFile(String message, Boolean isNewLine = true)
        {
            //creates directory if not exist
            Utils.GetOrCreateDirectory(LogDirectoryPath);

            var fileFullPath = Path.Combine(LogDirectoryPath, LogFileNameWithExtension);

            // Append new text to an existing file.
            // The using statement automatically flushes AND CLOSES the stream and calls 
            // IDisposable.Dispose on the stream object.
            FileStream fileStream = await Utils.WaitFileTillReadyToAppend(fileFullPath);
            if (fileStream != null)
            {
                using (fileStream)
                {
                    using (StreamWriter streamWriter = new StreamWriter(fileStream))
                    {
                        message = String.Format("{0} {1}", DateTime.Now.ToMessageHeaderString(), message);
                        if (isNewLine)
                        {
                            streamWriter.WriteLine(message);
                        }
                        else
                        {
                            streamWriter.Write(message);
                        }
                        streamWriter.Flush();
                        fileStream.Flush();
                    }
                }
            }
        }
    }

}
