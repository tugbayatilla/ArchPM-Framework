using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchPM.ApiQuery.AuthConfigSection.Model
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Configuration.ConfigurationElement" />
    public class EnvironmentInfoElement : ConfigurationElement
    {
        /// <summary>
        /// Gets or sets the env.
        /// </summary>
        /// <value>
        /// The env.
        /// </value>
        [ConfigurationProperty("Env", DefaultValue = AuthConfigSectionEnvironments.UAT, IsRequired = true)]
        [TypeConverter(typeof(CaseInsensitiveEnumConfigConverter<AuthConfigSectionEnvironments>))]
        public AuthConfigSectionEnvironments Env
        {
            get
            {
                return (AuthConfigSectionEnvironments)this["Env"];
            }
            set
            {
                this["Env"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the application.
        /// </summary>
        /// <value>
        /// The application.
        /// </value>
        [ConfigurationProperty("Application", DefaultValue = "", IsRequired = true)]
        public String Application
        {
            get
            {
                return (String)this["Application"];
            }
            set
            {
                this["Application"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the URL.
        /// </summary>
        /// <value>
        /// The URL.
        /// </value>
        [ConfigurationProperty("Url", DefaultValue = "", IsRequired = true)]
        public String Url
        {
            get
            {
                return (string)this["Url"];
            }
            set
            {
                this["Url"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the port.
        /// </summary>
        /// <value>
        /// The port.
        /// </value>
        [ConfigurationProperty("Port", DefaultValue = (int)4040, IsRequired = true)]
        [IntegerValidator(MinValue = 0, MaxValue = 65535, ExcludeRange = false)]
        public Int32 Port
        {
            get
            {
                return (Int32)this["Port"];
            }
            set
            {
                this["Port"] = value;
            }
        }
    }

}
