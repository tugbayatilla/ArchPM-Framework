using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchPM.ApiQuery.Tests.Model
{
    class InsertSample1Request : ApiQueryRequest
    {
        public override string ProcedureName => "TATILLA.PCK_APIQUERY.INSERT_SAMPLE1";

        [ApiQueryFieldThrowExceptionWhen(On = "Length", Operator = ApiQueryFieldOperators.GreaterThan, Value = 50)]
        [ApiQueryFieldThrowExceptionWhen(Operator = ApiQueryFieldOperators.EqualTo, Value = null)]
        [InputApiQueryField("STRING_VALUE_NOTNULL")]
        public String StringValueNotNull { get; set; }

        [ApiQueryFieldThrowExceptionWhen(On = "Length", Operator = ApiQueryFieldOperators.GreaterThan, Value = 50)]
        [InputApiQueryField("STRING_VALUE_NULL")]
        public String StringValueNull { get; set; }

        [ApiQueryFieldThrowExceptionWhen(Operator = ApiQueryFieldOperators.LessThan | ApiQueryFieldOperators.EqualTo, Value = 0)]
        [InputApiQueryField("NUMBER_VALUE_NOTNULL")]
        public Int32 NumberValueNotNull { get; set; }

        [ApiQueryFieldThrowExceptionWhen(Operator = ApiQueryFieldOperators.LessThan | ApiQueryFieldOperators.EqualTo, Value = 0)]
        [InputApiQueryField("NUMBER_VALUE_NULL")]
        public Int32? NumberValueNull { get; set; }
    }
}
