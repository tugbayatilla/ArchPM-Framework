﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchPM.Core
{
    public interface IBasicLog
    {
        void Log(Exception ex);
        void Log(String message);
    }
}