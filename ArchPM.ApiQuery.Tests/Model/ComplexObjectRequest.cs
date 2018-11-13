using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchPM.ApiQuery.Tests.Model
{
    class ComplexObjectRequest : ApiQueryRequest
    {
        [InputApiQueryField("PI_DATA")]
        public int Id { get; set; }

        public override string ProcedureName => "TATILLA.PCK_APIQUERY.COMPLEX_OBJECT"; 
    }


    class ComplexObjectResponse
    {
        [OutputApiQueryField("PO_SAMPLE1_ID")]
        public Int32? Id { get; set; }

        [OutputApiQueryField("PO_1ROW_LIST")]
        public Complex1 Complex { get; set; }

        [OutputApiQueryField("PO_MULTIROW_LIST")]
        public List<ComplexList2> List2 { get; set; }
    }

    class Complex1
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

    class ComplexList2
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
