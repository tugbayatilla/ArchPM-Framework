using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchPM.ApiQuery.Tests.Model
{
    class GetObjectRequest : ApiQueryRequest
    {
        [InputApiQueryField("PI_SAMPLE1_ID")]
        public int Id { get; set; }

        public override string ProcedureName => "TATILLA.PCK_APIQUERY.GET_OBJECT"; 
    }

    class GetObjectResponse
    {
        [OutputApiQueryField("PO_STRING_VALUE_NOTNULL")]
        public String StringValueNotNull { get; set; }

        [OutputApiQueryField("PO_NUMBER_VALUE_NOTNULL")]
        public int NumberValueNotNull { get; set; }
    }


}
