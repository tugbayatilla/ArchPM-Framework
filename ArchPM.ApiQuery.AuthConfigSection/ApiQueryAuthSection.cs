using ArchPM.ApiQuery.AuthConfigSection.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchPM.ApiQuery.AuthConfigSection
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Configuration.ConfigurationSection" />
    public class ApiQueryAuthSection : ConfigurationSection
    {
        // Create a "font" element.
        /// <summary>
        /// Gets or sets the auths.
        /// </summary>
        /// <value>
        /// The auths.
        /// </value>
        [ConfigurationProperty("Auths", IsRequired = true)]
        [ConfigurationCollection(typeof(AuthElementCollection),
        AddItemName = "Auth")]
        public AuthElementCollection Auths
        {
            get
            {
                return (AuthElementCollection)this["Auths"];
            }
            set
            {
                this["Auths"] = value;
            }
        }

        // Create a "color element."
        /// <summary>
        /// Gets or sets the environment infos.
        /// </summary>
        /// <value>
        /// The environment infos.
        /// </value>
        [ConfigurationProperty("EnvironmentInfos")]
        [ConfigurationCollection(typeof(EnvironmentInfoElementCollection),
        AddItemName = "EnvironmentInfo")]
        public EnvironmentInfoElementCollection EnvironmentInfos
        {
            get
            {
                return (EnvironmentInfoElementCollection)this["EnvironmentInfos"];
            }
            set
            { this["EnvironmentInfos"] = value; }
        }
    }

   
    

}
