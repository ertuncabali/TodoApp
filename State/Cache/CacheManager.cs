using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace State.Cache
{

    //Bu classları daha önce hazırladığım için üzerinde iyileştirme yapmıyorum.
    internal sealed class CacheManagement
    {
        private static object _lock = new object();
        private static CacheManagement cacheManager = null;
        private static ObjectCache objectCache = null;


        private CacheManagement()
        {

        }

        private static CacheManagement Initialize()
        {
            lock (_lock)
            {
                if (cacheManager == null)
                {
                    cacheManager = new CacheManagement();
                    objectCache = MemoryCache.Default;
                }
            }

            return cacheManager;
        }

        internal static CacheManagement Current
        {
            get
            {
                if (cacheManager != null)
                {
                    return cacheManager;
                }
                else
                {
                    return Initialize();
                }
            }
        }


        internal void Add(string Key, object Value)
        {
            this.Add(Key, Value, Defaults.CacheSlidingTimeSpan());
        }

        internal void Add(string Key, object Value, TimeSpan SlidingTimeSpan)
        {
            Remove(Key);
            if (Value != null)
            {
                var itemPolicy = new CacheItemPolicy() { SlidingExpiration = SlidingTimeSpan };
                objectCache.Add(Key, Value, itemPolicy);
            }
        }

        internal void Add(string Key, object Value, DateTime AbsoluteExpiration)
        {
            Remove(Key);
            if (Value != null)
            {
                var itemPolicy = new CacheItemPolicy() { AbsoluteExpiration = AbsoluteExpiration };
                objectCache.Add(Key, Value, itemPolicy);
            }
        }

        internal void Remove(string Key)
        {
            var selectedCache = objectCache.FirstOrDefault(x => x.Key == Key);
            if (selectedCache.Key != null)
            {
                objectCache.Remove(selectedCache.Key);
            }
        }

        internal void RemoveList(List<string> KeyList)
        {
            Func<KeyValuePair<string, object>, bool> criter = x => KeyList.Contains(x.Key);
            Func<KeyValuePair<string, object>, string> select = x => x.Key;

            var selectedCache = objectCache.Where(criter).Select(select).ToList();
            foreach (var item in selectedCache)
            {
                objectCache.Remove(item);
            }
        }

        internal object Get(string Key)
        {
            try
            {
                return objectCache.Get(Key);
            }
            catch (Exception)
            {
                return null;
            }
        }

        internal T Get<T>(string Key)
        {
            return (T)objectCache.Get(Key);
        }

        internal List<string> ListKeys()
        {
            List<string> AllKeys = objectCache.Select(x => x.Key).ToList();
            return AllKeys;
        }
    }
    public static class CacheManager
    {
        private static string CachePreKey = "GG4HH3NM";

        private static string GenerateCacheKey(string Key)
        {
            return string.Concat(CachePreKey, Key);
        }

        public static void Add(string Key, object Value)
        {
            CacheManager.Add(Key, Value, Defaults.CacheAbsoluteExpireTime());
        }

        public static void Add(string Key, object Value, TimeSpan SlidingTimeSpan)
        {
            string cacheKey = GenerateCacheKey(Key);
            CacheManagement.Current.Add(cacheKey, Value, SlidingTimeSpan);
        }

        public static void Add(string Key, object Value, DateTime AbsoluteExpiration)
        {
            string cacheKey = GenerateCacheKey(Key);
            CacheManagement.Current.Add(cacheKey, Value, AbsoluteExpiration);
        }

        public static T Get<T>(string Key)
        {
            var cacheObject = Get(Key);
            return cacheObject != null ? (T)cacheObject : default(T);
        }

        public static object Get(string Key)
        {
            string cacheKey = GenerateCacheKey(Key);

            var cache = CacheManagement.Current.Get(cacheKey);

            return cache;
        }

        public static List<string> ListKeys()
        {
            List<string> AllKeys = CacheManagement.Current.ListKeys()
                    .Where(x => x.StartsWith(CachePreKey))
                    .Select(x => x.Remove(0, CachePreKey.Length))
                    .ToList();

            return AllKeys;
        }

        public static bool Remove(string Key)
        {
            string cacheKey = GenerateCacheKey(Key);

            var fullNamedKeysList = CacheManagement.Current.ListKeys();

            if (fullNamedKeysList.Contains(cacheKey))
            {
                CacheManagement.Current.Remove(cacheKey);
                return true;
            }
            else
            {
                return false;
            }
        }

        public static void Clear()
        {
            var keyList = ListKeys();

            foreach (var item in keyList)
            {
                Remove(item);
            }
        }
    }
    public static class Defaults
    {

        public static TimeSpan CacheSlidingTimeSpan()
        {
            return TimeSpan.FromHours(1);
        }

        public static DateTime CacheAbsoluteExpireTime()
        {
            return CacheAbsoluteExpireAferMinute(25);
        }

        public static DateTime CacheAbsoluteExpireAferMinute(int minute)
        {
            return DateTime.Now.AddMinutes(minute);
        }
    }
}
