using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchPM.ApiQuery
{
    public abstract class ApiQueryRequest
    {
        public abstract String ProcedureName { get; }
        public virtual QueryResponseTypes? ResponseType { get; set; }

        public virtual void Validate()
        {
            //fistan
        }

    }
}
