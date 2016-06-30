using System;
using System.Collections.Generic;
using Contracts.Retail.Communication;
using Contracts.Retail.Domain;
using Impl.Server;

namespace Retail.SmallTests.Stubs
{
    public class MockConfigurationProvider : IConfigurationProvider
    {
        public List<Store> GetStores(Guid id)
        {
            return new List<Store>
            {
                (new StoreDto {Id = 1}).ToDomain(),
                (new StoreDto {Id = 2}).ToDomain(),
                (new StoreDto {Id = 3}).ToDomain()
            };
        }
    }
}