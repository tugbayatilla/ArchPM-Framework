using System;
using System.Configuration;

namespace ArchPM.Data
{
    public class AppConfigManager
    {
        public static String GetConfig(String name, String defaultValue)
        {
            String lowerName = name.ToLower();
            String result = ConfigurationManager.AppSettings.Get(lowerName);
            if (String.IsNullOrEmpty(result))
                result = defaultValue;

            return result;
        }

    }
}
