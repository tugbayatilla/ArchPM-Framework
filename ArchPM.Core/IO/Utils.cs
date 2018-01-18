﻿using ArchPM.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchPM.Core.IO
{
    public class Utils
    {
        /// <summary>
        /// Waits file to be ready or returns null
        /// </summary>
        /// <param name="fileFullPath"></param>
        /// <param name="waitMiliseconds"></param>
        /// <param name="sleepOnCatchMiliseconds"></param>
        /// <returns></returns>
        public static async Task<FileStream> WaitFileTillReadyToAppend(String fileFullPath, Int32 waitMiliseconds = 10000, Int32 sleepOnCatchMiliseconds = 100)
        {
            return await Task.Factory.StartNew<FileStream>(() =>
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();
                while (sw.ElapsedMilliseconds <= waitMiliseconds)
                {
                    try
                    {
                        // If the file can be opened for exclusive access it means that the file
                        // is no longer locked by another process.
                        FileStream inputStream = Utils.GetOrCreateFile(fileFullPath);
                        if (!inputStream.CanWrite)
                            continue;

                        var len = inputStream.Length > 0;

                        return inputStream;
                    }
                    catch
                    {
                        Task.Delay(sleepOnCatchMiliseconds);
                    }
                }
                sw.Stop();
                return null;
            });
        }

        public static DirectoryInfo GetOrCreateDirectory(String directoryPath)
        {
            DirectoryInfo result = null;

            if (!Directory.Exists(directoryPath))
            {
                result = Directory.CreateDirectory(directoryPath);
            }
            else
            {
                result = new DirectoryInfo(directoryPath);
            }

            return result;
            
        }

        /// <summary>
        /// Gets or creates if not exist a file given path
        /// </summary>
        /// <param name="fileFullPath">Fullpath of the file. </param>
        /// <returns>Open File Stream</returns>
        public static FileStream GetOrCreateFile(String fileFullPath)
        {
            FileMode mode = FileMode.CreateNew;
            //if file exist, change it to append mode
            if (File.Exists(fileFullPath))
            {
                mode = FileMode.Append;
            }

            return File.Open(fileFullPath, mode, FileAccess.Write, FileShare.None);
        }

        /// <summary>
        /// returns yyyyMMdd.txt according to datetime now
        /// </summary>
        /// <returns></returns>
        public static String GenerateDateTimeBasedFileNameWithExtension()
        {
            var now = DateTime.Now;
            return String.Format("{0}{1:D2}{2:D2}.txt", now.Year, now.Month, now.Day);
        }
    }

}
