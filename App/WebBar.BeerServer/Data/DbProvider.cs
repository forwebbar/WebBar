using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using Contracts.Common;
using Contracts.Common.AppServer;
using Impl.DAL;
using WebBar.BeerServer.Security;

namespace WebBar.BeerServer.Data
{
    internal class DbProvider : IBeerData
    {
        private ISecurityProvider _security;

        public DbProvider(ISecurityProvider security)
        {
            _security = security;
        }

        public List<MarketDto> GetMarkets(UserPass user)
        {
            if(!_security.Check(user))
                throw new AuthenticationException();

            var idUser = _security.GetUserId(user);
            var list = new List<MarketDto>();
            using (var context = new BeerControlEntities())
            {
                var dbMarkets = context.Market.Where(m => m.idUser == idUser);
                foreach (var dbMarket in dbMarkets)
                    list.Add(DtoFactory.Create(dbMarket));
            }

            return list;
        }

        public List<DrinkDto> GetDrinks(UserPass user)
        {
            if (!_security.Check(user))
                throw new AuthenticationException();

            var idUser = _security.GetUserId(user);
            var list = new List<DrinkDto>();
            using (var context = new BeerControlEntities())
            {
                var dbDrinks = context.Drink.Where(d => d.idUser == idUser);
                foreach (var dbDrink in dbDrinks)
                {
                    list.Add(DtoFactory.Create(dbDrink));
                }
            }

            return list;
        }

        public Dictionary<MarketDto, SellSummaryDto> GetSellSummary(UserPass user, DateTimeOffset startTs, DateTimeOffset endTs)
        {
            if (!_security.Check(user))
                throw new AuthenticationException();

            var idUser = _security.GetUserId(user);
            var items = new Dictionary<MarketDto, SellSummaryDto>();
            using (var context = new BeerControlEntities())
            {
                var dbMarkets = context.Market.Where(m => m.idUser == idUser).ToArray();
                foreach (var dbMarket in dbMarkets)
                    items.Add(DtoFactory.Create(dbMarket), DoGetMarketSellSummary(context, dbMarket.id, startTs, endTs));
            }

            return items;
        }

        private SellSummaryDto DoGetMarketSellSummary(BeerControlEntities context, int idMarket, DateTimeOffset startTs, DateTimeOffset endTs)
        {
            var dto = new SellSummaryDto();
            var sells = context.Sell.Where(s => s.idMarket == idMarket)// && s.Ts >= startTs && s.Ts <= endTs)
                .Select(s => new {s.Sum, s.Volume, s.isCleaning}).ToArray();

            dto.Fill = (uint) sells.Sum(s => s.Volume);
            dto.Money = (uint)sells.Where(s=>s.isCleaning == false).Sum(s => s.Sum);

            return dto;
        }

        public Dictionary<DrinkDto, List<SellDto>> GetMarketSells(UserPass user, int idMarket, DateTimeOffset startTs, DateTimeOffset endTs)
        {
            if (!_security.Check(user))
                throw new AuthenticationException();

            var items = new Dictionary<DrinkDto, List<SellDto>>();
            using (var context = new BeerControlEntities())
            {
                var drinkIds = context.MarketDrink.Where(m => m.idMarket == idMarket).Select(d=> d.idDrink).ToArray();
                var dbDrinks = context.Drink.Where(d => drinkIds.Contains(d.id));
                foreach (var dbDrink in dbDrinks)
                    items.Add(DtoFactory.Create(dbDrink), DoGetDrinkSells(context, idMarket, dbDrink.id, startTs, endTs));
            }

            return items;
        }

        public Dictionary<MarketDto, List<SellDto>> GetDrinkSells(UserPass user, int idDrink, DateTimeOffset startTs, DateTimeOffset endTs)
        {
            if (!_security.Check(user))
                throw new AuthenticationException();

            var items = new Dictionary<MarketDto, List<SellDto>>();
            using (var context = new BeerControlEntities())
            {
                var marketIds = context.MarketDrink.Where(m => m.idDrink == idDrink).Select(d => d.idDrink).ToArray();
                var dbMarkets = context.Market.Where(m => marketIds.Contains(m.id)).ToArray();
                foreach (var dbMarket in dbMarkets)
                    items.Add(DtoFactory.Create(dbMarket),
                        DoGetDrinkSells(context, dbMarket.id, idDrink, startTs, endTs));
            }

            return items;
        }

        private List<SellDto> DoGetDrinkSells(BeerControlEntities context, int idMarket, int idDrink, DateTimeOffset startTs, DateTimeOffset endTs)
        {
            var list = new List<SellDto>();
            var dbSells = context.Sell.Where(s => s.idMarket == idMarket && s.idDrink == idDrink && s.Ts >= startTs && s.Ts <= endTs).OrderBy(s=>s.Ts).ToArray();
            var priceIds = dbSells.Select(s => s.idPrice).Distinct();
            var dbPrices = context.Price.Where(p => priceIds.Contains(p.id)).Select(p => new {p.id, p.Val});
            var priceDic = new Dictionary<int, int>();
            foreach (var dbPrice in dbPrices)
                priceDic.Add(dbPrice.id, dbPrice.Val);

            foreach (var dbSell in dbSells)
            {
                int price;
                if (dbSell.idPrice == 0 || !priceDic.ContainsKey(dbSell.idPrice))
                    price = 0;
                else
                    price = priceDic[dbSell.idPrice];

                list.Add(DtoFactory.Create(dbSell, price));
            }

            return list;
        }

        public Dictionary<DrinkDto, SellSummaryDto> GetMarketSummary(UserPass user, int marketId, DateTimeOffset startTs, DateTimeOffset endTs)
        {
            throw new NotImplementedException();
        }

        public void SetSellStatus(UserPass user, SellDto sell, bool isCleaning)
        {
            if (!_security.Check(user))
                throw new AuthenticationException();

            using (var context = new BeerControlEntities())
            {
                var dbSell = context.Sell.FirstOrDefault(s=>s.id == sell.Id);
                if(dbSell == null)
                    return;

                dbSell.isCleaning = isCleaning;
                context.SaveChanges();
            }
        }
    }
}