﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchPM.Win32
{
    public enum ProcessDpiAwareness
    {
        /// <summary>
        /// Process is not DPI aware.
        /// </summary>
        DpiUnaware = 0,
        /// <summary>
        /// Process is system DPI aware (= WPF default).
        /// </summary>
        SystemDpiAware = 1,
        /// <summary>
        /// Process is per monitor DPI aware (Win81+ only).
        /// </summary>
        PerMonitorDpiAware = 2
    }
}
