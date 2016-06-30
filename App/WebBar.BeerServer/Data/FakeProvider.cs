using System;
using System.Collections.Generic;
using Contracts.Common;
using Contracts.Common.AppServer;

namespace WebBar.BeerServer.Data
{
    internal class FakeProvider : IBeerData
    {
        private readonly List<MarketDto> _markets;
        private readonly List<DrinkDto> _drinks;
        private Dictionary<int, uint> _drinkPrice = new Dictionary<int, uint>();

        public FakeProvider()
        {
            _markets = new List<MarketDto>()
            {
                new MarketDto
                {
                    Id = 1,
                    Name = "Магазин #1"
                },
                new MarketDto
                {
                    Id = 2,
                    Name = "Магазин #2"
                },
            };

            _drinks = new List<DrinkDto>()
            {
                new DrinkDto
                {
                    Id = 1,
                    Name = "Балтика №7"
                },

                new DrinkDto
                {
                    Id = 2,
                    Name = "Жигулевское светлое"
                },

                new DrinkDto
                {
                    Id = 3,
                    Name = "Жигулевское темное"
                },
            };

            uint i = 1;
            foreach (var drink in _drinks)
            {
                _drinkPrice.Add(drink.Id, 100*100*(i++));
            }
        }

        public List<MarketDto> GetMarkets(UserPass user)
        {            
            return _markets;
        }

        public List<DrinkDto> GetDrinks(UserPass user)
        {
            return _drinks;
        }

        public Dictionary<MarketDto, SellSummaryDto> GetSellSummary(UserPass user, DateTimeOffset startTs, DateTimeOffset endTs)
        {
            var rez = new Dictionary<MarketDto, SellSummaryDto>();
            uint i = (uint)(endTs - startTs).TotalDays;
            foreach (var market in _markets)
            {
                rez.Add(market, new SellSummaryDto
                {
                    Money = 150000*i,
                    Fill = 20000 * i,
                    Img = null,
                    MoneyShift = (int) (10* i),
                    FillShift = (int) (-5* i),
                    MoneyDelta = 98*i
                });

                i++;
            }

            return rez;
        }

        public Dictionary<MarketDto, SellSummaryDto> GetDrinkSummary(UserPass user, int drinkId, DateTimeOffset startTs, DateTimeOffset endTs)
        {
            var rez = new Dictionary<MarketDto, SellSummaryDto>();

            uint i = (uint) (endTs-startTs).TotalDays;
            foreach (var market in _markets)
            {
                uint u = (uint) (i*drinkId);
                rez.Add(market, new SellSummaryDto
                {
                    Money = 7000 * u,
                    Fill = 1000 * u,
                    Img = null,
                    MoneyShift = (int)(-9 * u),
                    FillShift = (int)(3 * u),
                    MoneyDelta = 17 * u
                });

                u++;
            }

            return rez;
        }

        public Dictionary<DrinkDto, SellSummaryDto> GetMarketSummary(UserPass user, int marketId, DateTimeOffset startTs, DateTimeOffset endTs)
        {
            var rez = new Dictionary<DrinkDto, SellSummaryDto>();
            uint i = (uint)(endTs - startTs).TotalDays;

            foreach (var drink in _drinks)
            {
                rez.Add(drink, new SellSummaryDto
                {
                    Money = 13800*i,
                    Fill = 9661*i,
                    Img = null,
                    MoneyShift = (int) (7*i),
                    FillShift = (int) (1*i),
                    MoneyDelta = 32*i
                });

                i++;
            }

            return rez;
        }

        public Dictionary<DrinkDto, List<SellDto>> GetMarketSells(UserPass user, int marketId, DateTimeOffset startTs, DateTimeOffset endTs)
        {
            var rez = new Dictionary<DrinkDto, List<SellDto>>();

            foreach (var drink in _drinks)
                rez.Add(drink, CreateFakeSells(drink.Id, startTs, endTs));

            return rez;
        }

        public Dictionary<MarketDto, List<SellDto>> GetDrinkSells(UserPass user, int drinkId, DateTimeOffset startTs, DateTimeOffset endTs)
        {
            var rez = new Dictionary<MarketDto, List<SellDto>>();

            foreach (var market in _markets)
                rez.Add(market, CreateFakeSells(drinkId, startTs, endTs));

            return rez;
        }

        public void SetSellStatus(UserPass user, SellDto sell, bool isCleaning)
        {
            return;
        }

        private List<SellDto> CreateFakeSells(int drinkId, DateTimeOffset startTs, DateTimeOffset endTs)
        {
            var list = new List<SellDto>();
            var days = (endTs - startTs).TotalDays;

            var hourStep = 4;
            int count = (int) (days*hourStep);
            for (int i = 0; i < count; i++)
            {
                DateTimeOffset ts = startTs.AddHours(hourStep*i);
                list.Add(CreateRandomSell(ts, drinkId));
            }

            return list;
        }

        private SellDto CreateRandomSell(DateTimeOffset ts, int drinkId)
        {
            var sell = new SellDto();
            var rand = new Random(ts.Hour);

            sell.Id = 1000*rand.Next();
            sell.Fill = (uint) (1000*rand.NextDouble()*1.5);
            sell.IsCleaning = rand.NextDouble() > 0.5;
            sell.Price = GetPrice(drinkId);
            sell.Money = (uint) (((sell.Fill/1000.0)*(sell.Price/100.0))*100);
            sell.MoneyDelta = (uint)( (rand.NextDouble() *200 ) + (rand.NextDouble() * 100));
            sell.IsMoneyDeltaRed = rand.NextDouble() > 0.5;
            sell.Ts = ts;

            return sell;
        }

        private uint GetPrice(int drinkId)
        {
            return _drinkPrice[drinkId];
        }
    }
}