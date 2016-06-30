using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Hosting;
using Contracts.Common.AppServer;
using WebBar.Site.Enum;
using WebBar.Site.Repositories;

namespace WebBar.Site.Models
{
    public class SaleViewModel
    {
        #region Constructors

        public SaleViewModel(MainShopRepository shopRepository)
        {
            if (shopRepository == null)
                throw new ArgumentNullException(nameof(shopRepository));

            ShopRepository = shopRepository;
        }

        #endregion

        #region Fields
        public byte[] FakePng = System.IO.File.ReadAllBytes(HostingEnvironment.MapPath(@"~\Images\fakePlot.png"));

        #endregion

        #region Properties

        public MainShopRepository ShopRepository { get; }
        public int? CurrentShopId { get; set; }
        public string CurrentShopAddress { get; set; }
        public DateTime? FilterDateStart { get; set; }
        public DateTime? FilterDateEnd { get; set; }
        public int? SelectedBeerType { get; set; }
        public string SelectedBeerName { get; set; }
        public string DateTimeFilterType { get; set; }
        public double TotalRub { get; set; }
        public double TotalLitres { get; set; }
        public Dictionary<MarketDto, SellSummaryDto> SellSummaries { get; set; }
        public string Report1CFileName { get; set; }
        public string ReportExcelFileName { get; set; }
        public bool IsDetail { get; set; }

        public bool? IsBeerTypeCleaning { get; set; }
        public int? IdBeerType { get; set; }
        public long? IdSummary { get; set; }


        #endregion

        #region Methods



        public static DateTimeInterval GetInterval(string filterType)
        {
            var now = DateTime.Now;
            switch (filterType)
            {
                default:
                    return new DateTimeInterval(new DateTime(now.Year, now.Month, now.Day), now);
                case FilterTypeEnum.Month:
                    return new DateTimeInterval(now.AddMonths(-1), now);
                case FilterTypeEnum.Quater:
                    return new DateTimeInterval(now.AddMonths(-3), now);
                case FilterTypeEnum.Week:
                    return new DateTimeInterval(now.AddDays(-7), now);
                case FilterTypeEnum.Year:
                    return new DateTimeInterval(now.AddYears(-1), now);
                case FilterTypeEnum.Yesterday:
                {
                    var midnight = new DateTime(now.Year, now.Month, now.Day);
                    return new DateTimeInterval(midnight.AddDays(-1), midnight);
                }

            }
        }

        public void UpdateShopViews()
        {
            if (DateTimeFilterType == FilterTypeEnum.Custom)
            {
                if (FilterDateStart == null || FilterDateEnd == null)
                {
                    FilterDateStart = DateTime.Now.AddYears(-1);
                    FilterDateEnd = DateTime.Now;
                }

                SellSummaries = ShopRepository.GetShopViews(FilterDateStart.Value, FilterDateEnd.Value,
                    ShopRepository.GetShops().FirstOrDefault(a => a.Id == CurrentShopId),
                    ShopRepository.GetBeerTypes().FirstOrDefault(a => a.Id == SelectedBeerType));
            }

            else
            {
                var interval = GetInterval(DateTimeFilterType);
                FilterDateStart = interval.DateStart;
                FilterDateEnd = interval.DateEnd;

                SellSummaries = ShopRepository.GetShopViews(FilterDateStart.Value, FilterDateEnd.Value,
                    ShopRepository.GetShops().FirstOrDefault(a => a.Id == CurrentShopId),
                    ShopRepository.GetBeerTypes().FirstOrDefault(a => a.Id == SelectedBeerType));//TODO Убрать это дублирование
            }

          

            TotalLitres = SellSummaries.Sum(a => a.Value.Fill)/1000D;
            TotalRub = SellSummaries.Sum(a => a.Value.Money)/100D;

            Report1CFileName = ShopRepository.GetReportFileName1C(FilterDateStart.Value, FilterDateEnd.Value,
                ShopRepository.GetShops().FirstOrDefault(a => a.Id == CurrentShopId),
                ShopRepository.GetBeerTypes().FirstOrDefault(a => a.Id == SelectedBeerType));
            ReportExcelFileName = ShopRepository.GetReportFileNameExcel(FilterDateStart.Value, FilterDateEnd.Value,
                ShopRepository.GetShops().FirstOrDefault(a => a.Id == CurrentShopId),
                ShopRepository.GetBeerTypes().FirstOrDefault(a => a.Id == SelectedBeerType));
        }

        public ShopView[] GetShopViews()
        {
            var res = CurrentShopId != null
                ? SellSummaries.Where(a => a.Key.Id == CurrentShopId)
                    .Select(a => new ShopView {Shop = a.Key, Summary = a.Value})
                    .ToArray()
                : SellSummaries.Select(a => new ShopView {Shop = a.Key, Summary = a.Value}).ToArray();

            return res;
        }
        public DrinkView[] GetDetailViews()
        {
            var curShop = ShopRepository.GetShops().FirstOrDefault(a => a.Id == CurrentShopId);

            if (CurrentShopId == null)
                return null;

            var curDrink = ShopRepository.GetShopBeerTypes(curShop).FirstOrDefault(a => a.Id == SelectedBeerType);

            var res = curDrink != null
                ? ShopRepository.GetDrinkCash(curDrink.Id, FilterDateStart.GetValueOrDefault(),
                    FilterDateEnd.GetValueOrDefault()).Where(a => a.Key.Id == CurrentShopId)
                    .SelectMany(a => a.Value.Select(b => new DrinkView {Drink = curDrink, Summary = b}))
                    .ToArray()
                : ShopRepository.GetShopCash(CurrentShopId.Value, FilterDateStart.GetValueOrDefault(),
                    FilterDateEnd.GetValueOrDefault())
                    .SelectMany(a => a.Value.Select(b => new DrinkView {Drink = a.Key, Summary = b}))
                    .ToArray();
            return res;
        }

        #endregion
    }

    public class ShopView
    {
        public MarketDto Shop { get; set; }
        public SellSummaryDto Summary { get; set; }
    }

    public class DrinkView
    {
        public DrinkDto Drink { get; set; }
        public SellDto Summary { get; set; }
    }

}