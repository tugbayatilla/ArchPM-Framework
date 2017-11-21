using System;
using System.Configuration;

namespace ArchPM.Data
{
    /// <summary>
    /// 
    /// </summary>
    public class AppConfigManager
    {
        /// <summary>
        /// Gets the configuration.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>returns the given name in the config otherwise returns defaultvalue</returns>
        public static String GetConfig(String name, String defaultValue)
        {
            String result = ConfigurationManager.AppSettings.Get(name);
            if (String.IsNullOrEmpty(result))
                result = defaultValue;

            return result;
        }

    }
}
