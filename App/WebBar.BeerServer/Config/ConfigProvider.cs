using System;
using System.Collections.Generic;
using System.Linq;
using Contracts.Common;
using Contracts.Common.AppServer;
using WebBar.BeerServer.Security;
using System.Security.Authentication;
using Impl.DAL;

namespace WebBar.BeerServer.Config
{
    internal class ConfigProvider : IBeerConfig
    {
        private ISecurityProvider _security;

        public ConfigProvider(ISecurityProvider security)
        {
            _security = security;
        }

        public List<DrinkConfigDto> GetDrinksConfig(UserPass user)
        {
            if (!_security.Check(user))
                throw new AuthenticationException();

            var idUser = _security.GetUserId(user);
            var drinks = new List<DrinkConfigDto>();
            using (var context = new BeerControlEntities())
            {
                var producerNames = GetProducerNames(idUser, context);
                var dbDrinks = context.Drink.Where(d => d.idUser == idUser);
                foreach(var drink in dbDrinks)
                {
                    drinks.Add(DtoFactory.Create(drink, producerNames));
                }
            }

            return drinks;
        }

        private static Dictionary<int, string> GetProducerNames(int idUser, BeerControlEntities context)
        {
            var producerNames = context.Producer.Where(p => p.idUser == idUser).Select(p => new { p.id, p.Name }).ToArray();
            var producerDic = new Dictionary<int, string>();
            foreach (var pr in producerNames)
            {
                if (producerDic.ContainsKey(pr.id))
                    continue;

                producerDic.Add(pr.id, pr.Name);
            }

            return producerDic;
        }

        public void AddDrinkConfig(UserPass user, DrinkConfigDto drinkConfig)
        {
            if (!_security.Check(user))
                throw new AuthenticationException();

            var idUser = _security.GetUserId(user);
            using (var context = new BeerControlEntities())
            {
                var producerNames = GetProducerNames(idUser, context);
                if (!producerNames.ContainsKey(drinkConfig.IdProducer))
                    drinkConfig.IdProducer = 0;

                if (string.IsNullOrEmpty(drinkConfig.Code))
                    drinkConfig.Code = string.Format("D{0:D3}", context.Drink.Count(d=>d.idUser == idUser));

                context.Drink.Add(new Drink
                {
                    Name = drinkConfig.Name,
                    Code = drinkConfig.Code,
                    idProducer = drinkConfig.IdProducer,
                    idUser = idUser
                });
                context.SaveChanges();
            }
        }

        public void EditDrinkConfig(UserPass user, DrinkConfigDto drinkConfig)
        {
            //TODO Сделать правку сорта
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
            if (!_security.Check(user))
                throw new AuthenticationException();

            var idUser = _security.GetUserId(user);
            var prices = new Dictionary<MarketDto, PriceDto>();
            using (var context = new BeerControlEntities())
            {
                var dbMarket = context.Market.Where(m => m.idUser == idUser).Select(m=>m.id).ToList();
                var dbPrice = context.Price.Where(m => m.idDrink == idDrink && m.idMarket != null && dbMarket.Contains((int)m.idMarket));

                foreach(var pr in dbPrice)
                {

                }
            }

            return prices;
        }

        public void SetDrinkPrices(UserPass user, int idDrink, Dictionary<int, PriceDto> marketPrices)
        {
            throw new System.NotImplementedException();
        }

        public List<ProducerDto> GetProducers(UserPass user)
        {
            if (!_security.Check(user))
                throw new AuthenticationException();

            var idUser = _security.GetUserId(user);
            var list = new List<ProducerDto>();
            using (var context = new BeerControlEntities())
            {
                var dbProducers = context.Producer.Where(p => p.idUser == idUser);
                foreach (var pr in dbProducers)
                    list.Add(DtoFactory.Create(pr));
            }

            return list;
        }

        public void AddProducer(UserPass user, ProducerDto producer)
        {
            if (!_security.Check(user))
                throw new AuthenticationException();

            var idUser = _security.GetUserId(user);
            using (var context = new BeerControlEntities())
            {
                context.Producer.Add(CreateDbProducer(context, producer, idUser));
                context.SaveChanges();
            }
        }

        public void EditProducer(UserPass user, ProducerDto dto)
        {
            if (!_security.Check(user))
                throw new AuthenticationException();

            var idUser = _security.GetUserId(user);
            using (var context = new BeerControlEntities())
            {
                var dbProducer = context.Producer.Find(dto.Id);

                var producer = CreateDbProducer(context, dto, idUser);
                dbProducer.Account = producer.Account;
                dbProducer.Address = producer.Address;
                dbProducer.ActualAddress = producer.ActualAddress;
                dbProducer.ActualDate = producer.ActualDate;
                dbProducer.Bank = producer.Bank;
                dbProducer.Bik = producer.Bik;
                dbProducer.Code = producer.Code;
                dbProducer.INN = producer.INN;
                dbProducer.Kpp = producer.Kpp;
                dbProducer.Name = producer.Name;
                dbProducer.Ogrn = producer.Ogrn;

                context.SaveChanges();
            }           
        }

        private static Producer CreateDbProducer(BeerControlEntities context, ProducerDto producer, int idUser)
        {
            if (string.IsNullOrEmpty(producer.Code))
                producer.Code = string.Format("P{0:D3}", context.Producer.Count(d => d.idUser == idUser));

            return new Producer
            {
                Account = producer.Account,
                Address = producer.LawAdress,
                ActualAddress = producer.ActualAdress,
                ActualDate = producer.ActualDate,
                Bank = producer.Bank,
                Bik = producer.Bik,
                Code = producer.Code,
                idUser = idUser,
                INN = producer.Inn,
                Kpp = producer.Kpp,
                Name = producer.Name,
                Ogrn = producer.Ogrn
            };
        }


        public List<MarketConfigDto> GetMarketsConfig(UserPass user)
        {
            if (!_security.Check(user))
                throw new AuthenticationException();

            var idUser = _security.GetUserId(user);
            var list = new List<MarketConfigDto>();
            using (var context = new BeerControlEntities())
            {
                var dbMarket = context.Market.Where(m => m.idUser == idUser);
                foreach(Market mr in dbMarket)
                    list.Add(DtoFactory.CreateEx(mr));
            }

            return list;
        }

        public void AddMarketConfig(UserPass user, MarketDetailConfigDto dto)
        {
            if (!_security.Check(user))
                throw new AuthenticationException();

            var idUser = _security.GetUserId(user);
            using (var context = new BeerControlEntities())
            {
                var dbDevice = context.Device.FirstOrDefault(d => d.Uid == dto.DeviceSerial);
                if(dbDevice != null)
                    throw new Exception("Device allready exists");

                if (string.IsNullOrEmpty(dto.Code))
                    dto.Code = string.Format("M{0:D3}", context.Market.Count(d => d.idUser == idUser));

                var market = new Market
                {
                    Address = dto.Address,
                    Name = dto.Name,
                    Code = dto.Code,
                    idUser = idUser
                };
                context.Market.Add(market);

                context.SaveChanges();

                dbDevice = CreateDevice(market.id, dto);
                context.Device.Add(dbDevice);
                context.SaveChanges();
            }

        }

        public MarketDetailConfigDto GetMarketDetailConfig(UserPass user, int idMarket)
        {
            if (!_security.Check(user))
                throw new AuthenticationException();

            var idUser = _security.GetUserId(user);
            using (var context = new BeerControlEntities())
            {
                var dto = new MarketDetailConfigDto();
                var dbMarket = context.Market.Find(idMarket);
                dto.Id = idMarket;
                dto.Name = dbMarket.Name;
                dto.Code = dbMarket.Code;
                dto.Address = dbMarket.Address;

                var dbDevice = context.Device.FirstOrDefault(d => d.idMarket == idMarket);
                if (dbDevice != null)
                {
                    dto.DeviceSerial = dbDevice.Uid;
                    dto.Drinks = GetDrinks(context, dbDevice.id);
                }

                return dto;
            }
        }

        private List<MarketDrinkListItemDto> GetDrinks(BeerControlEntities context, long idDevice)
        {
            var drinks = new List<MarketDrinkListItemDto>();
            var tapDrinks = new Dictionary<int, MarketDrinkListItemDto>();
            for (int i = 0; i < 30; i++)
            {
                int tapCode = i + 1;
                tapDrinks.Add(tapCode, new MarketDrinkListItemDto() { TapCode = tapCode });
            }

            var dbDrinks = context.DeviceTap.Where(d => d.idDevice == idDevice).ToArray();
            foreach(var dbDr in dbDrinks)
            {
                MarketDrinkListItemDto item;
                if (!tapDrinks.ContainsKey(dbDr.TapCode))
                    continue;

                item = tapDrinks[dbDr.TapCode];
                item.CurDrink = LoadDrink(context, dbDr.idDrink);
                item.FutureDrink = LoadDrink(context, dbDr.idFutureDrink);
                if(dbDr.FutureDrinkDate.HasValue)
                    item.FutureDrinkDate = dbDr.FutureDrinkDate.Value;
            }

            foreach(var tapDrink in tapDrinks)
                drinks.Add(tapDrink.Value);

            return drinks;
        }

        private DrinkDto LoadDrink(BeerControlEntities context, int? idDrink)
        {
            if (!idDrink.HasValue)
                return null;

            var dbDrink = context.Drink.Find(idDrink);
            return DtoFactory.Create(dbDrink);
        }

        public void SetMarketDetailConfig(UserPass user, int idMarket, MarketDetailConfigDto marketConfig)
        {
            if (!_security.Check(user))
                throw new AuthenticationException();

            var idUser = _security.GetUserId(user);
            using (var context = new BeerControlEntities())
            {
                var dbDevice = context.Device.FirstOrDefault(d => d.idMarket == idMarket);
                if (dbDevice == null)
                {
                    dbDevice = CreateDevice(idMarket, marketConfig);
                    context.Device.Add(dbDevice);
                }

                dbDevice.Uid = marketConfig.DeviceSerial;

                var dbMarket = context.Market.Find(idMarket);
                dbMarket.Address = marketConfig.Address;
                if(!string.IsNullOrEmpty(marketConfig.Name))
                    dbMarket.Name = marketConfig.Name;

                context.SaveChanges();

                foreach (var tabDrink in marketConfig.Drinks)
                {
                    var dbTabDrink = context.DeviceTap.FirstOrDefault(d => d.idDevice == dbDevice.id && d.TapCode == tabDrink.TapCode);
                    if (dbTabDrink == null)
                    {
                        dbTabDrink = CreateTabDrink(dbDevice.id, tabDrink);
                        context.DeviceTap.Add(dbTabDrink);
                    }
                        
                    dbTabDrink.idFutureDrink = tabDrink.NewDrink !=null ? tabDrink.NewDrink.Id : (int?) null;
                    dbTabDrink.FutureDrinkDate = tabDrink.FutureDrinkDate;
                }

                context.SaveChanges();

                RefreshMarketDrinks(context, idMarket, marketConfig.Drinks);
            }
        }

        private Device CreateDevice(int idMarket, MarketDetailConfigDto marketConfig)
        {
            return new Device
            {
                idMarket = idMarket,
                Uid = marketConfig.DeviceSerial,
                Name = "Device_" + marketConfig.DeviceSerial
            };
        }

        private DeviceTap CreateTabDrink(long idDevice, MarketDrinkListItemDto tabDrink)
        {
            return new DeviceTap
            {
                idDevice = idDevice,
                idDrink = tabDrink.CurDrink != null ? tabDrink.CurDrink.Id : (int?)null,
                TapCode = tabDrink.TapCode,
            };
        }

        private void RefreshMarketDrinks(BeerControlEntities context, int idMarket, List<MarketDrinkListItemDto> drinks)
        {
            var drinkIdArr = context.MarketDrink.Where(m => m.idMarket == idMarket).Select(d=>d.idDrink).ToArray();
            HashSet<int> drinkIds = new HashSet<int>();

            foreach(var drink in drinks)
            {
                if(drink.CurDrink != null)
                    drinkIds.Add(drink.CurDrink.Id);

                if(drink.NewDrink != null)
                    drinkIds.Add(drink.NewDrink.Id);
            }

            foreach(var fromDbDrinkId in drinkIdArr)
            {
                if (drinkIds.Contains(fromDbDrinkId))
                    drinkIds.Remove(fromDbDrinkId);
            }

            if (!drinkIds.Any())
                return;

            foreach (var idDrink in drinkIds)
            {
                context.MarketDrink.Add(new MarketDrink
                {
                    idMarket = idMarket,
                    idDrink = idDrink
                });
            }

            context.SaveChanges();
        }
    }
}