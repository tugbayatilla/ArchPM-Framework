using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchPM.ApiQuery
{
    /// <summary>
    /// Output parameters
    /// </summary>
    public class TestResponse1
    {
        [QueryField("PO_FIRMNAME")]
        public String FirmName { get; set; }

        [QueryField("PO_FIRMID")]
        public Int64 FirmId { get; set; }

        [QueryField("PO_BRANCHNAME")]
        public String BranchName { get; set; }

        [QueryField("PO_BRANCHID")]
        public Int64 BranchId { get; set; }
    }

    /// <summary>
    /// Input Parameters
    /// </summary>
    public class TestRequest1
    {
        [QueryField("PI_CARDID")]
        public String CardId { get; set; }
    }
}
