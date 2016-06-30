using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Contracts.Common;
using Contracts.Retail.Domain;

namespace Impl.Client
{
    public class InMemorCache : IRepositoryCache
    {
        private readonly ConcurrentDictionary<Guid, List<Store>> _cacheData = new ConcurrentDictionary<Guid, List<Store>>();
        private readonly IConfigurationProvider _configurationProvider;

        public InMemorCache(IConfigurationProvider configurationProvider)
        {
            _configurationProvider = configurationProvider;
        }

        public List<Store> GetItems(UserPass pass)
        {
            var id = pass.UId;
            return !Contains(id) ? new List<Store>() : _cacheData[id];
        }

        private bool Contains(Guid id)
        {            
            if (_cacheData.ContainsKey(id))
                return true;

            var stores = _configurationProvider.GetStores(id);
            if (stores == null || !stores.Any())
                return false;

            return _cacheData.TryAdd(id, stores);
        }
    }
}
