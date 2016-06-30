using System;
using System.Collections.Generic;
using System.ServiceModel;
using Contracts.Common;
using Contracts.Common.AppServer;

namespace WebBar.BeerServer.Data
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    public class BeerDataService : IBeerData
    {
        private readonly IDataProviderFactory _factory = new ProviderFactory();

        public List<MarketDto> GetMarkets(UserPass user)
        {
            IBeerData provider = _factory.GetOrCreateProvider(user);
            return provider.GetMarkets(null);
        }

        public List<DrinkDto> GetDrinks(UserPass user)
        {
            IBeerData provider = _factory.GetOrCreateProvider(user);
            return provider.GetDrinks(null);
        }

        public Dictionary<MarketDto, SellSummaryDto> GetSellSummary(UserPass user, DateTimeOffset startTs, DateTimeOffset endTs)
        {
            IBeerData provider = _factory.GetOrCreateProvider(user);
            return provider.GetSellSummary(null, startTs, endTs);
        }

        public Dictionary<DrinkDto, SellSummaryDto> GetMarketSummary(UserPass user, int marketId, DateTimeOffset startTs, DateTimeOffset endTs)
        {
            IBeerData provider = _factory.GetOrCreateProvider(user);
            return provider.GetMarketSummary(null, marketId, startTs, endTs);
        }

        public Dictionary<DrinkDto, List<SellDto>> GetMarketSells(UserPass user, int marketId, DateTimeOffset startTs, DateTimeOffset endTs)
        {
            IBeerData provider = _factory.GetOrCreateProvider(user);
            return provider.GetMarketSells(null, marketId, startTs, endTs);
        }

        public Dictionary<MarketDto, List<SellDto>> GetDrinkSells(UserPass user, int drinkId, DateTimeOffset startTs, DateTimeOffset endTs)
        {
            IBeerData provider = _factory.GetOrCreateProvider(user);
            return provider.GetDrinkSells(null, drinkId, startTs, endTs);
        }

        public void SetSellStatus(UserPass user, SellDto sell, bool isCleaning)
        {
            IBeerData provider = _factory.GetOrCreateProvider(user);
            provider.SetSellStatus(null, sell, isCleaning);
        }
    }
}
