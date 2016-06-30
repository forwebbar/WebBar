using System;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBar.Site.Enum;
using WebBar.Site.Models;
using WebBar.Site.Security;
using static System.String;

namespace WebBar.Site.Controllers
{
    [TokenAuthorize]
    public class HomeController : BaseController
    {

        #region Constructors

        public HomeController(SaleViewModel model)
        {
            _model = model; 
        }

        #endregion




        #region Fields

        private readonly SaleViewModel _model;

        #endregion

        protected void StripQueryStringAndRedirect(string[] keysToRemove)
        {
            var queryString = new NameValueCollection(HttpContext.Request.QueryString);

            foreach (var key in keysToRemove)
                queryString.Remove(key);

            var newQueryString = "";

            for (var i = 0; i < queryString.Count; i++)
            {
                if (i > 0) newQueryString += "&";
                newQueryString += queryString.GetKey(i) + "=" + queryString[i];
            }

            var newPath = HttpContext.Request.Path + (!string.IsNullOrEmpty(newQueryString) ? "?" + newQueryString : string.Empty);

            if (HttpContext.Request.Url != null && HttpContext.Request.Url.PathAndQuery != newPath)
                HttpContext.Response.Redirect(newPath, true);
        }

        public ActionResult Index(int? beerType = null, int? shopId = null, string filterType = FilterTypeEnum.Today,
            DateTime? filterDateStart = null,
            DateTime? filterDateEnd = null, bool isDetail = false, bool? isBeerTypeCleaning=null, int? idBeerType=null, long? idSummary = null)
        {
            var repository = _model.ShopRepository;
            var beerTypes = repository.GetBeerTypes();
            var shops = repository.GetShops();
            _model.DateTimeFilterType = filterType;
            _model.IsDetail = isDetail;
            _model.IsBeerTypeCleaning = isBeerTypeCleaning;
            _model.IdBeerType = idBeerType;
            _model.IdSummary = idSummary;


            _model.SelectedBeerType = beerTypes.Where(a => a.Id == beerType).Select(a=>a.Id).FirstOrDefault();

            if (shopId == null)
            {
                _model.CurrentShopId = null;
                _model.CurrentShopAddress = null;
            }
            else
            {
                var shop = shops.FirstOrDefault(a => a.Id == shopId);

                if (shop != null)
                {
                    _model.CurrentShopId = shop.Id;
                    _model.CurrentShopAddress = shop.Name;
                }
            }


            if (beerType == null)
            {
                _model.SelectedBeerType = null;
                _model.SelectedBeerName = null;
            }
            else
            {
                var beerTypeInstance = beerTypes.FirstOrDefault(a => a.Id == beerType);

                if (beerTypeInstance != null)
                {
                    _model.SelectedBeerType = beerTypeInstance.Id;
                    _model.SelectedBeerName = beerTypeInstance.Name;
                }
            }

            if (filterDateStart != null)
                _model.FilterDateStart = filterDateStart.Value;

            if (filterDateEnd != null)
                _model.FilterDateEnd = filterDateEnd.Value;

            _model.UpdateShopViews();

            if (_model.IsBeerTypeCleaning != null && _model.IdBeerType != null && _model.IdSummary != null)
            {
                var sell =
                    _model.GetDetailViews()
                        .Where(a => a.Drink.Id == _model.IdBeerType && a.Summary.Id == _model.IdSummary)
                        .Select(a => a.Summary)
                        .FirstOrDefault();

                if (sell != null)
                {
                    _model.ShopRepository.SetSellStatus(sell,
                        _model.IsBeerTypeCleaning != null && _model.IsBeerTypeCleaning.Value);
                }
                _model.IsBeerTypeCleaning = null;
                _model.IdBeerType = null;
                _model.IdSummary = null;

                StripQueryStringAndRedirect(new[] {"idBeerType", "isBeerTypeCleaning"});
            }

            return View(_model);
        }
    }
}