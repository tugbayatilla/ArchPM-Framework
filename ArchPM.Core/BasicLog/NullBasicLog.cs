using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchPM.Core
{
    public sealed class NullBasicLog : IBasicLog
    {
        public void Log(Exception ex)
        {
        }

        public void Log(string message)
        {
        }
    }
}
