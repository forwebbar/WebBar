using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Contracts.Common;
using Contracts.Common.AppServer;
using WebBar.Site.Controllers;
using WebBar.Site.ServiceClients;

namespace WebBar.Site.Repositories
{
    public class MainShopRepository
    {
        #region Constructors

        public MainShopRepository(WcfServiceClient<IBeerData> clientProxyBeerData, WcfServiceClient<IBeerConfig> clientProxyBeerConfig)
        {
            if (clientProxyBeerData == null)
                throw new ArgumentNullException(nameof(clientProxyBeerData));
            if (clientProxyBeerConfig == null)
                throw new ArgumentNullException(nameof(clientProxyBeerConfig));

            _clientProxyBeerData = clientProxyBeerData;
            _clientProxyBeerConfig = clientProxyBeerConfig;
        }

        #endregion

        #region Fields
        private readonly WcfServiceClient<IBeerData> _clientProxyBeerData;
        private readonly WcfServiceClient<IBeerConfig> _clientProxyBeerConfig;

        #endregion

        public UserPass UserPass => HttpContext.Current?.Session[ControllerExtentions.UserTokenParamName] as UserPass;

        public WcfServiceClient<IBeerData> ClientProxyBeerData
        {
            get { return _clientProxyBeerData; }
        }

        public WcfServiceClient<IBeerConfig> ClientProxyBeerConfig
        {
            get { return _clientProxyBeerConfig; }
        }

        #region Methods

        public Dictionary<MarketDto, SellSummaryDto> GetShopViews(DateTime dateStart, DateTime dateEnd, MarketDto shop, DrinkDto beerType)
        {
            return _clientProxyBeerData.Execute(a => a.GetSellSummary(UserPass, dateStart, dateEnd));
        }

        public string GetReportFileName1C(DateTime dateStart, DateTime dateEnd, MarketDto shop, DrinkDto beerType)
        {
            return "fakePdf.pdf";
        }

        public string GetReportFileNameExcel(DateTime dateStart, DateTime dateEnd, MarketDto shop, DrinkDto beerType)
        {
            return "fakeExcel.xlsx";
        }

        public SellSummaryDto[] GetShopBeerValues(int shopId, DrinkDto beerType, DateTime dateStart, DateTime dateEnd)
        {
            return _clientProxyBeerData.Execute(a => a.GetMarketSummary(UserPass, shopId, dateStart, dateEnd)
                .Where(b => b.Key.Id == beerType.Id)
                .Select(b => b.Value)
                .ToArray());
        }

        public DrinkDto[] GetBeerTypes()
        {
            return _clientProxyBeerData.Execute(a => a.GetDrinks(UserPass).ToArray());
        }

        public DrinkDto[] GetShopBeerTypes(MarketDto shop)
        {
            return GetBeerTypes();
        }

        public MarketDto[] GetShops()
        {
            return _clientProxyBeerData.Execute(a => a.GetMarkets(UserPass).ToArray());
        }

        public Dictionary<DrinkDto, List<SellDto>> GetShopCash(int shopId, DateTimeOffset dateStart, DateTimeOffset dateEnd)
        {
            return _clientProxyBeerData.Execute(a => a.GetMarketSells(UserPass, shopId, dateStart, dateEnd));
        }
        public Dictionary<MarketDto, List<SellDto>> GetDrinkCash(int drinkId, DateTimeOffset dateStart, DateTimeOffset dateEnd)
        {
            return _clientProxyBeerData.Execute(a => a.GetDrinkSells(UserPass, drinkId, dateStart, dateEnd));
        }
        public void SetSellStatus(SellDto sellItem, bool isCleaning)
        {
            _clientProxyBeerData.Execute(a => a.SetSellStatus(UserPass, sellItem, isCleaning));
        }

        public DrinkConfigDto[] GetConfigBeerTypes()
        {
            return _clientProxyBeerConfig.Execute(a => a.GetDrinksConfig(UserPass).ToArray());
        }

        #endregion
    }
}
