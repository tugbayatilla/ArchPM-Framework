using ArchPM.Core;
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
    public class ApiQuerySectionManager
    {
        /// <summary>
        /// Gets the section.
        /// </summary>
        /// <value>
        /// The section.
        /// </value>
        public ApiQueryAuthSection Section { get; private set; }

        /// <summary>
        /// Initializes the specified section name.
        /// </summary>
        /// <param name="sectionName">Name of the section.</param>
        /// <returns></returns>
        public static ApiQuerySectionManager Initialize(String sectionName = "ApiQueryAuthSection")
        {
            ApiQuerySectionManager manager = new ApiQuerySectionManager();

            // Get the current configuration file.
            System.Configuration.Configuration config =
                    ConfigurationManager.OpenExeConfiguration(
                    ConfigurationUserLevel.None);

            // Get the custom configuration section.
            ApiQueryAuthSection section = config.GetSection(sectionName) as ApiQueryAuthSection;
            section.ThrowExceptionIfNull($"Section '{sectionName}' is not defined in configSections!");

            manager.Section = section;

            return manager;
        }
    }
}
