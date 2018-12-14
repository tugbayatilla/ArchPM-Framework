using ArchPM.Core.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchPM.Core.Tests.ApiHelpTestObjects
{
    public class Class_WithoutApiHelp
    {
        [ApiHelp]
        public int IntProperty_WithApiHelp { get; set; }
        [ApiHelp]
        public String StringProperty_WithApiHelp { get; set; }
        [ApiHelp]
        public DateTime DateTimeProperty_WithApiHelp { get; set; }

        public int IntProperty_WithoutApiHelp { get; set; }
        public String StringProperty_WithoutApiHelp { get; set; }
        public DateTime DateTimeProperty_WithoutApiHelp { get; set; }

        [ApiHelp]
        public void Method_WithApiHelp_WithRequestAsObject_WithoutResponse(Class_WithApiHelp request)
        {
        }

        [ApiHelp]
        public void Method_WithApiHelp_WithoutRequest_WithoutResponse()
        {
        }

        [ApiHelp]
        public int Method_WithApiHelp_WithoutRequest_WithResponse()
        {
            return 1;
        }

        [ApiHelp]
        public void Method_WithApiHelp_WithRequestAsValue_WithoutResponse(Int32 request)
        {
        }

        public void Method_WithoutApiHelp_WithRequestAsObject_WithoutResponse(Class_WithApiHelp request)
        {
        }

        public void Method_WithoutApiHelp_WithoutRequest_WithoutResponse()
        {
        }

        public int Method_WithoutApiHelp_WithoutRequest_WithResponse()
        {
            return 1;
        }

        public void Method_WithoutApiHelp_WithRequestAsValue_WithoutResponse(Int32 request)
        {
        }
    }
}
