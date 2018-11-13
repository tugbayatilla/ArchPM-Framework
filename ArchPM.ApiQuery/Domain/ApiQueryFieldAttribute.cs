using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchPM.ApiQuery
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class ApiQueryFieldAttribute : Attribute
    {
        public ApiQueryFieldAttribute(String name) 
        {
            this.Name = name;
            this.Direction = ApiDbParameterDirection.Input;
        }

        public ApiQueryFieldAttribute(String name, ApiDbType dbType) : this(name) //fistan: delete me later
        {
            this.DbType = dbType;
        }

        public String Name { get; private set; }
        public ApiDbParameterDirection? Direction { get; set; }
        public ApiDbType? DbType { get; set; }
        public Int32? Size { get; set; }

    }

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
