using Noqoush.Framework.Caching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Noqoush.AdFalcon.Server.BillingController.Server.Models;

namespace Noqoush.AdFalcon.Server.BillingController.Server
{
    internal class BillingControllerCache: IBillingControllerCache
    {
        private ICacheProvider _cacheProvider;
        private TimeSpan _defaultCachingPeriod;
        private const string _availableFundKeyPrefix = "AvailableFund";

        public BillingControllerCache()
        {
            _cacheProvider = CacheManager.Current[Constants.Commons.BillingControllerCacheName, CacheStores.Memory];
            _defaultCachingPeriod = TimeSpan.FromMinutes(1);
        }

        public bool Add<T>(T billingEntity) where T : IBillingEntity
        {
            var key = GetPrefixedKey<T>(billingEntity.Id);
            return _cacheProvider.Put(key, billingEntity, _defaultCachingPeriod);
        }

        public bool AddAvailableFund(int accountId, decimal value)
        {
            var key = GetPrefixedKey(_availableFundKeyPrefix, accountId);
            return _cacheProvider.Put(key, value, _defaultCachingPeriod);
        }

        public decimal GetAvailableFund(int accountId)
        {
            var key = GetPrefixedKey(_availableFundKeyPrefix, accountId);
            return _cacheProvider.Get<decimal>(key);
        }

        public T Get<T>(int billingEntityId) where T : IBillingEntity
        {
            var key = GetPrefixedKey<T>(billingEntityId);
            return _cacheProvider.Get<T>(key);
        }

        public bool Remove<T>(int billingEntityId) where T : IBillingEntity
        {
            var key = GetPrefixedKey<T>(billingEntityId);
            return _cacheProvider.Remove(key);
        }

        private string GetPrefixedKey<T>(int Id)
        {
            return $"{typeof(T).Name}.{Id.ToString()}";
        }

        private string GetPrefixedKey(string prefix,int Id)
        {
            return $"{prefix}.{Id.ToString()}";
        }
    }
}
