using System.Collections.Generic;
using Contracts.Common;
using Contracts.Retail.Domain;

namespace Impl.Client
{
    /// <summary>
    /// Репозиторий магазинов
    /// </summary>
    // ReSharper disable once ClassNeverInstantiated.Global
    public class StoreRepository : IRepository
    {
        private IRepositoryCache _cache;
        private IDataProvider _dataProvider;

        public StoreRepository(IRepositoryCache cache, IDataProvider dataProvider)
        {
            _cache = cache;
            _dataProvider = dataProvider;
        }

        public List<Store> GetStores(UserPass pass)
        {
            var stores = _cache.GetItems(pass);
            _dataProvider.Update(stores);
            return stores;
        }

        public void Dispose()
        {        
            _cache = null;
            _dataProvider = null;
        }
    }
}