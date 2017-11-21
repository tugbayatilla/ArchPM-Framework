﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArchPM.Web.Core.Managers
{
    public class CacheManager
    {
        public static void ClearAll()
        {
            List<string> cacheList = (from DictionaryEntry cache in HttpRuntime.Cache select cache.Key.ToString()).ToList();
            cacheList.ForEach(c => HttpRuntime.Cache.Remove(c));
        }
    }
}
