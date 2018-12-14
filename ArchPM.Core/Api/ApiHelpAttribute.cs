using ArchPM.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchPM.Core.Api
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Attribute" />
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property, AllowMultiple = false)]
    public class ApiHelpAttribute : Attribute
    {
        /// <summary>Initializes a new instance of the <see cref="ApiHelpAttribute"/> class.</summary>
        public ApiHelpAttribute()
        {

        }

        /// <summary>Initializes a new instance of the <see cref="ApiHelpAttribute"/> class.</summary>
        /// <param name="comment">The comment.</param>
        public ApiHelpAttribute(String comment)
        {
            this.Comment = comment;
        }
        /// <summary>
        /// Gets or sets the comment.
        /// </summary>
        /// <value>
        /// The comment.
        /// </value>
        public String Comment { get; set; }
    }
}
