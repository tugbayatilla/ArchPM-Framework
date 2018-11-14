using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchPM.ApiQuery
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Attribute" />
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class ApiQueryFieldAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApiQueryFieldAttribute"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public ApiQueryFieldAttribute(String name) 
        {
            this.Name = name;
            this.Direction = ApiDbParameterDirection.Input;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiQueryFieldAttribute"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="dbType">Type of the database.</param>
        public ApiQueryFieldAttribute(String name, ApiDbType dbType) : this(name)
        {
            this.DbType = dbType;
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public String Name { get; private set; }
        /// <summary>
        /// Gets or sets the direction.
        /// </summary>
        /// <value>
        /// The direction.
        /// </value>
        public ApiDbParameterDirection? Direction { get; set; }
        /// <summary>
        /// Gets or sets the type of the database.
        /// </summary>
        /// <value>
        /// The type of the database.
        /// </value>
        public ApiDbType? DbType { get; set; }
        /// <summary>
        /// Gets or sets the size.
        /// </summary>
        /// <value>
        /// The size.
        /// </value>
        public Int32? Size { get; set; }

    }

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="ArchPM.ApiQuery.ApiQueryFieldAttribute" />
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Class, AllowMultiple = false)]
    public class OutputApiQueryFieldAttribute : ApiQueryFieldAttribute
    {
        public OutputApiQueryFieldAttribute(String name) : base(name)
        {
            this.Direction = ApiDbParameterDirection.Output;
        }

        public OutputApiQueryFieldAttribute(String name, ApiDbType dbType) : base(name, dbType)
        {
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="ArchPM.ApiQuery.ApiQueryFieldAttribute" />
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class InputApiQueryFieldAttribute : ApiQueryFieldAttribute
    {
        public InputApiQueryFieldAttribute(String name) : base(name)
        {
            this.Direction = ApiDbParameterDirection.Input;
        }

        public InputApiQueryFieldAttribute(String name, ApiDbType dbType) : base(name, dbType)
        {
        }
    }
}
