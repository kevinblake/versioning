using System;
using System.Web;
using System.Web.Caching;

namespace Puzzlebox.Versioning.Business.CacheHelpers
{
	public class InMemoryCache : ICacheService
	{
		public T Get<T>(string cacheId, Func<T> getItemCallback) where T : class
		{
			return Get(cacheId, null, System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromHours(1), getItemCallback);
		}

		public T Get<T>(string cacheId, CacheDependency cacheDependency, DateTime absoluteExpiration,
		                TimeSpan slidingExpiration, Func<T> getItemCallback) where T : class
		{
			var item = HttpRuntime.Cache.Get(cacheId) as T;
			if (item == null)
			{
				item = getItemCallback();
				HttpContext.Current.Cache.Insert(cacheId, item, cacheDependency, absoluteExpiration, slidingExpiration);
			}
			return item;
		}
	}

	internal interface ICacheService
	{
		T Get<T>(string cacheId, Func<T> getItemCallback) where T : class;

		T Get<T>(string cacheId, CacheDependency cacheDependency, DateTime absoluteExpiration, TimeSpan slidingExpiration,
		         Func<T> getItemCallback) where T : class;
	}
}