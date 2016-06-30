using System;
using System.Collections.Generic;
using Contracts.Common.AppServer;
using Impl.DAL;

namespace WebBar.BeerServer
{
    internal static class DtoFactory
    {
        public static MarketDto Create(Market dbMarket)
        {
            return new MarketDto
            {
                Id = dbMarket.id,
                Name = dbMarket.Name
            };
        }

        public static MarketConfigDto CreateEx(Market dbMarket)
        {
            return new MarketConfigDto
            {
                Id = dbMarket.id,
                Name = dbMarket.Name,
                Address = dbMarket.Address,
                Code = dbMarket.Code
            };
        }

        public static DrinkDto Create(Drink dbDrink)
        {
            return new DrinkDto
            {
                Id = dbDrink.id,
                Name = dbDrink.Name
            };
        }

        public static SellDto Create(Sell x, int price)
        {
            return new SellDto
            {
                Id = x.id,
                Ts = x.Ts,
                Money = (uint) x.Sum,
                Fill =  (uint) x.Volume,
                Price = (uint) price,
                IsCleaning = x.isCleaning
            };
        }

        internal static DrinkConfigDto Create(Drink drink, Dictionary<int, string> producerNames)
        {
            var producerName = producerNames.ContainsKey(drink.idProducer) ? producerNames[drink.idProducer] : "?";
            return new DrinkConfigDto
            {
                Id = drink.id,
                Code = drink.Code,
                IdProducer = drink.idProducer,
                Name = drink.Name,
                ProducerName = producerName
            };
        }

        internal static ProducerDto Create(Producer pr)
        {
            return new ProducerDto
            {
                Id = pr.id,
                Account = pr.Account,
                ActualDate = pr.ActualDate??DateTimeOffset.Now,
                Bank = pr.Bank,
                Bik = pr.Bik,
                Code = pr.Code,
                Inn = pr.INN,
                Kpp = pr.Kpp,
                Name = pr.Name,
                Ogrn = pr.Ogrn,
                ActualAdress = pr.ActualAddress,
                LawAdress = pr.Address
            };
        }
    }
}