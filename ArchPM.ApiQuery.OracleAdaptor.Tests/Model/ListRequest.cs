using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchPM.ApiQuery.OracleAdaptor.Tests.Model
{
    class SingleListRequest : ApiQueryRequest
    {
        [InputApiQueryField("PI_SAMPLE1_ID")]
        public int Id { get; set; }

        public override string ProcedureName => "TATILLA.PCK_APIQUERY.SINGLE_LIST"; //fistan: buna burada gerek yok. bunu engine uzerine alm
    }

    [OutputApiQueryField("PO_DATASET")]
    class SingleListResponse
    {
        [OutputApiQueryField("STRING_VALUE_NOTNULL")]
        public String StringValueNotNull { get; set; }

        [OutputApiQueryField("STRING_VALUE_NULL")]
        public String StringValueNull { get; set; }

        [OutputApiQueryField("NUMBER_VALUE_NOTNULL")]
        public Int32 NumberValueNotNull { get; set; }

        [OutputApiQueryField("NUMBER_VALUE_NULL")]
        public Int32? NumberValueNull { get; set; }
    }

    class SingleListResponseNoAttribute
    {
        [OutputApiQueryField("STRING_VALUE_NOTNULL")]
        public String StringValueNotNull { get; set; }

        [OutputApiQueryField("STRING_VALUE_NULL")]
        public String StringValueNull { get; set; }

        [OutputApiQueryField("NUMBER_VALUE_NOTNULL")]
        public Int32 NumberValueNotNull { get; set; }

        [OutputApiQueryField("NUMBER_VALUE_NULL")]
        public Int32? NumberValueNull { get; set; }
    }


}
