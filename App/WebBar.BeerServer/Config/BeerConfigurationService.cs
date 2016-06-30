using System.Collections.Generic;
using System.ServiceModel;
using Contracts.Common;
using Contracts.Common.AppServer;

namespace WebBar.BeerServer.Config
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    public class BeerConfigurationService : IBeerConfig
    {
        private readonly IConfigProviderFactory _factory = new ProviderFactory();

        public List<DrinkConfigDto> GetDrinksConfig(UserPass user)
        {
            var provider = _factory.GetOrCreateProvider(user);
            return provider.GetDrinksConfig(user);
        }

        public void AddDrinkConfig(UserPass user, DrinkConfigDto drinkConfig)
        {
            var provider = _factory.GetOrCreateProvider(user);
            provider.AddDrinkConfig(user, drinkConfig);
        }

        public void EditDrinkConfig(UserPass user, DrinkConfigDto drinkConfig)
        {
            //TODO сделать правку сорта
        }

        public void DeleteDrinkConfig(UserPass user, int drinkId)
        {
            //TODO Сделать удаление сорта
        }

        public void DeleteProducerConfig(UserPass user, int producerId)
        {
            //TODO Сделать удаление поставщика
        }

        public Dictionary<MarketDto, PriceDto> GetDrinkPrices(UserPass user, int idDrink)
        {
            var provider = _factory.GetOrCreateProvider(user);
            return provider.GetDrinkPrices(user, idDrink);
        }

        public void SetDrinkPrices(UserPass user, int idDrink, Dictionary<int, PriceDto> marketPrices)
        {
            var provider = _factory.GetOrCreateProvider(user);
            provider.SetDrinkPrices(user, idDrink, marketPrices);
        }

        public List<ProducerDto> GetProducers(UserPass user)
        {
            var provider = _factory.GetOrCreateProvider(user);
            return provider.GetProducers(user);
        }

        public void AddProducer(UserPass user, ProducerDto producer)
        {
            var provider = _factory.GetOrCreateProvider(user);
            provider.AddProducer(user, producer);
        }

        public void EditProducer(UserPass user, ProducerDto producer)
        {
            var provider = _factory.GetOrCreateProvider(user);
            provider.EditProducer(user, producer);
        }

        public List<MarketConfigDto> GetMarketsConfig(UserPass user)
        {
            var provider = _factory.GetOrCreateProvider(user);
            return provider.GetMarketsConfig(user);
        }

        public void AddMarketConfig(UserPass user, MarketDetailConfigDto marketConfig)
        {
            var provider = _factory.GetOrCreateProvider(user);
            provider.AddMarketConfig(user, marketConfig);
        }

        public MarketDetailConfigDto GetMarketDetailConfig(UserPass user, int idMarket)
        {
            var provider = _factory.GetOrCreateProvider(user);
            return provider.GetMarketDetailConfig(user, idMarket);
        }

        public void SetMarketDetailConfig(UserPass user, int idMarket, MarketDetailConfigDto marketConfig)
        {
            var provider = _factory.GetOrCreateProvider(user);
            provider.SetMarketDetailConfig(user, idMarket, marketConfig);
        }
    }
}