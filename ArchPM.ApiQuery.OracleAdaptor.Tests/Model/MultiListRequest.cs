using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchPM.ApiQuery.OracleAdaptor.Tests.Model
{
    class MultiListRequest : ApiQueryRequest
    {
        [InputApiQueryField("PI_SAMPLE1_ID")]
        public int Id { get; set; }

        public override string ProcedureName => "TATILLA.PCK_APIQUERY.MULTI_LIST"; //fistan: buna burada gerek yok. bunu engine uzerine alm
    }


    class MultiListResponse
    {
        //public MultiListResponse()
        //{
        //    List1 = new List<MultiList1>();
        //    List2 = new List<MultiList2>();
        //}

        [OutputApiQueryField("PO_DATASET")]
        public List<MultiList1> List1 { get; set; }

        [OutputApiQueryField("PO_DATASET2")]
        public List<MultiList2> List2 { get; set; }
    }

    class MultiList1
    {
        [OutputApiQueryField("SAMPLE3_ID")]
        public Int32 Sample3Id { get; set; }

        [OutputApiQueryField("STRING_VALUE_NOTNULL")]
        public String StringValueNotNull { get; set; }

        [OutputApiQueryField("STRING_VALUE_NULL")]
        public String StringValueNull { get; set; }

        [OutputApiQueryField("NUMBER_VALUE_NOTNULL")]
        public Int32 NumberValueNotNull { get; set; }

        [OutputApiQueryField("NUMBER_VALUE_NULL")]
        public Int32? NumberValueNull { get; set; }

        [OutputApiQueryField("SAMPLE1_ID")]
        public Int32? Sample1Id { get; set; }

        [OutputApiQueryField("SAMPLE2_ID")]
        public Int32? Sample2Id { get; set; }

    }

    class MultiList2
    {
        [OutputApiQueryField("SAMPLE3_ID")]
        public Int32 Sample3Id { get; set; }

        [OutputApiQueryField("STRING_VALUE_NOTNULL")]
        public String StringValueNotNull { get; set; }

        [OutputApiQueryField("STRING_VALUE_NULL")]
        public String StringValueNull { get; set; }

        [OutputApiQueryField("NUMBER_VALUE_NOTNULL")]
        public Int32 NumberValueNotNull { get; set; }

        [OutputApiQueryField("NUMBER_VALUE_NULL")]
        public Int32? NumberValueNull { get; set; }

        [OutputApiQueryField("SAMPLE1_ID")]
        public Int32? Sample1Id { get; set; }

        [OutputApiQueryField("SAMPLE2_ID")]
        public Int32? Sample2Id { get; set; }
    }




}
