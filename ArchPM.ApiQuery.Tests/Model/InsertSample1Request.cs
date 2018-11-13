using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchPM.ApiQuery.Tests.Model
{
    class InsertSample1Request
    {
        [InputApiQueryField("STRING_VALUE_NOTNULL")]
        public String StringValueNotNull { get; set; }

        [InputApiQueryField("STRING_VALUE_NULL")]
        public String StringValueNull { get; set; }

        [InputApiQueryField("NUMBER_VALUE_NOTNULL")]
        public Int32 NumberValueNotNull { get; set; }

        [InputApiQueryField("NUMBER_VALUE_NULL")]
        public Int32? NumberValueNull { get; set; }
    }
}
