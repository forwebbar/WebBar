using System;
using System.Collections.Generic;

namespace Contracts.Retail.Domain
{
    public interface IConfigurationProvider
    {
        List<Store> GetStores(Guid id);
    }
}