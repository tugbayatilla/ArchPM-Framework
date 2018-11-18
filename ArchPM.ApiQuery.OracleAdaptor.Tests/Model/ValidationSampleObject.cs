using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchPM.ApiQuery.OracleAdaptor.Tests.Model
{
    class ValidationSampleObject : ApiQueryRequest
    {
        public override string ProcedureName => "";

        [ApiQueryFieldThrowExceptionWhen(On ="Length", Operator = ApiQueryFieldOperators.GreaterThan, Value = 50)]
        [ApiQueryFieldThrowExceptionWhen(Operator = ApiQueryFieldOperators.EqualTo, Value = null)]
        public String StringPropertyAs50Chars { get; set; }

        [ApiQueryFieldThrowExceptionWhen(On ="Length", Operator = ApiQueryFieldOperators.GreaterThan, Value = 50)]
        public String StringPropertyAsNull { get; set; }
    }
}
