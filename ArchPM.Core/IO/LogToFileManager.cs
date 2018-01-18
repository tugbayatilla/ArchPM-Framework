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
    public class LogToFileManager
    {
        public String LogDirectoryPath { get; set; }
        public String LogFileNameWithExtension { get; set; }

        /// <summary>
        /// Gets and Sets Waiting milliseconds the file to be ready
        /// </summary>
        /// <value>Default is 10000ms (10s)</value>
        public Int32 WaitMilliseconds { get; set; }

        public LogToFileManager()
        {
            this.LogDirectoryPath = Path.Combine(Environment.CurrentDirectory, "Logs");
            this.LogFileNameWithExtension = Utils.GenerateDateTimeBasedFileNameWithExtension();
            this.WaitMilliseconds = 10000;
        }

        /// <summary>
        /// Writes log message to file. Creates the file if not exist
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task AppendToFile(String message)
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
                        streamWriter.WriteLine(message);
                        streamWriter.Flush();
                        fileStream.Flush();
                    }
                }
            }

        }
    }

}
