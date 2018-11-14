using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchPM.ApiQuery
{
    /// <summary>
    /// Throws Exception when given arguments met
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class ApiQueryFieldThrowExceptionWhenAttribute : Attribute
    {
        /// <summary>
        /// Gets or sets the error messsage.
        /// </summary>
        /// <value>
        /// The error messsage.
        /// </value>
        public String ErrorMesssage { get; set; }
        /// <summary>
        /// Gets or sets the operator.
        /// </summary>
        /// <value>
        /// The operator.
        /// </value>
        public ApiQueryFieldOperators Operator { get; set; }
        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public Object Value { get; set; }
        /// <summary>
        /// Property definition
        /// </summary>
        public String On { get; set; }
    }

   

}
