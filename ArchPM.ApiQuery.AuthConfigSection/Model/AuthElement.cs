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
    public class AuthElement : ConfigurationElement
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
        /// Gets or sets the service.
        /// </summary>
        /// <value>
        /// The service.
        /// </value>
        [ConfigurationProperty("Service", DefaultValue = "", IsRequired = true)]
        public String Service
        {
            get
            {
                return (String)this["Service"];
            }
            set
            {
                this["Service"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the action.
        /// </summary>
        /// <value>
        /// The action.
        /// </value>
        [ConfigurationProperty("Action", DefaultValue = "", IsRequired = true)]
        public String Action
        {
            get
            {
                return (String)this["Action"];
            }
            set
            {
                this["Action"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        /// <value>
        /// The username.
        /// </value>
        [ConfigurationProperty("Username", DefaultValue = "", IsRequired = true)]
        public String Username
        {
            get
            {
                return (String)this["Username"];
            }
            set
            {
                this["Username"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        [ConfigurationProperty("Password", DefaultValue = "", IsRequired = true)]
        public String Password
        {
            get
            {
                return (String)this["Password"];
            }
            set
            {
                this["Password"] = value;
            }
        }
    }


}
