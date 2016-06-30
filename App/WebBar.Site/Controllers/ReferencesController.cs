using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Contracts.Common;
using Contracts.Common.AppServer;
using Newtonsoft.Json;
using WebBar.Site.Models;
using WebBar.Site.Repositories;
using WebBar.Site.Security;
using WebBar.Site.ServiceClients;

namespace WebBar.Site.Controllers
{
    [TokenAuthorize]
    public class ReferencesController : BaseController
    {
        #region Constructors

        public ReferencesController(MainShopRepository repository)
        {
            _repository = repository;
        }

        #endregion

        #region Fields

        private readonly MainShopRepository _repository;

        #endregion

        private WcfServiceClient<IBeerConfig> ClientProxyBeerConfig => _repository.ClientProxyBeerConfig;

        private UserPass UserPass => _repository.UserPass;

        [HttpGet]
        public ActionResult EditDrinkConfig(int drinkId)
        {
            var result = new EditDrinksConfigViewModel
            {
                EditEntity =
                    ClientProxyBeerConfig.Execute(a => a.GetDrinksConfig(UserPass)).FirstOrDefault(a => a.Id == drinkId),
                Producers = ClientProxyBeerConfig.Execute(a => a.GetProducers(UserPass)).ToArray(),
                Prices =
                    ClientProxyBeerConfig.Execute(a => a.GetDrinkPrices(UserPass, drinkId))
                        .Select(a => new PriceViewModel {PriceDto = a.Value, MarketDto = a.Key})
                        .ToArray(),
                DrinkPriceStart = DateTime.Now.AddDays(1)
            };

            ClientProxyBeerConfig.Execute(a => a.GetDrinkPrices(UserPass, drinkId));
            return View(result);

        }

        [HttpGet]
        public ActionResult AddDrinkConfig()
        {
            var result = new EditDrinksConfigViewModel
            {
                EditEntity = new DrinkConfigDto(),
                Producers = ClientProxyBeerConfig.Execute(a => a.GetProducers(UserPass)).ToArray(),
            };
            result.DrinkPriceStart = DateTime.Now.AddDays(1);
            
            return View("EditDrinkConfig", result);

        }

        [HttpPost]
        public ActionResult AddDrinkConfig(EditDrinksConfigViewModel model)
        {
            if (ModelState.IsValid)
            {
                ClientProxyBeerConfig.Execute(a => a.AddDrinkConfig(UserPass, model.EditEntity));
                return RedirectToAction("Index", "References");
            }
            return RedirectToAction("Index", "References");
        }

        [HttpPost]
        public ActionResult EditDrinkConfig(EditDrinksConfigViewModel drC)
        {
            if (ModelState.IsValid)
            {
                ClientProxyBeerConfig.Execute(a => a.EditDrinkConfig(UserPass, drC.EditEntity));
            }
            return RedirectToAction("Index", "References");
        }

        [HttpPost]
        public ActionResult EditPrice(int idDrink, int idPrice, string value)
        {
            var valInt = int.Parse(value)/100;
            var prices = ClientProxyBeerConfig.Execute(a => a.GetDrinkPrices(UserPass, idDrink));

            var price = prices.First(a => a.Value.Id == idPrice);
            price.Value.FuturePrice = valInt;

            var dic = new Dictionary<int, PriceDto> {{idPrice, price.Value}};
            ClientProxyBeerConfig.Execute(a => a.SetDrinkPrices(UserPass, idDrink, dic));

            return null;
        }

       

        [HttpGet]
        public ActionResult DeleteDrinkConfig(int drinkId)
        {
            ClientProxyBeerConfig.Execute(a => a.DeleteDrinkConfig(UserPass, drinkId));
            return RedirectToAction("Index", "References");

        }

        [HttpGet]
        public ActionResult DeleteProducerConfig(int producerId)
        {
            ClientProxyBeerConfig.Execute(a => a.DeleteProducerConfig(UserPass, producerId));
            return RedirectToAction("Producers", "References");

        }

        public ActionResult Index()
        {
            return View(_repository);
        }

        public ActionResult Producers()
        {
            return View(_repository);
        }
        public ActionResult Shops()
        {
            return View(_repository);
        }

        [HttpGet]
        public ActionResult AddProducerConfig()
        {
            var producer = new ProducerDto {ActualDate = DateTime.Now.AddDays(1)};
            return View("EditProducerConfig", producer);
        }

        [HttpPost]
        public ActionResult AddProducerConfig(ProducerDto model)
        {
            if (ModelState.IsValid)
            {
                ClientProxyBeerConfig.Execute(a => a.AddProducer(UserPass, model));
            }
            return RedirectToAction("Producers", "References");
        }

        [HttpGet]
        public ActionResult EditProducerConfig(int producerId)
        {
            var result = ClientProxyBeerConfig.Execute(a => a.GetProducers(UserPass)).First(a => a.Id == producerId);
            return View(result);

        }

        [HttpPost]
        public ActionResult EditProducerConfig(ProducerDto model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    ClientProxyBeerConfig.Execute(a => a.EditProducer(UserPass, model));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("SaveError", ex);
                    return View(model);
                }
            }
            return RedirectToAction("Producers", "References");
        }

        [HttpGet]
        public ActionResult AddShopConfig()
        {
            var market = new ShopConfigViewModel
            {
                Entity = new MarketDetailConfigDto {ActualDate = DateTime.Now.AddDays(1)},
                Drinks = _repository.ClientProxyBeerData.Execute(a => a.GetDrinks(UserPass)).ToArray()
            };

            return View("EditShopConfig", market);
        }

        [HttpPost]
        public ActionResult AddShopConfig(ShopConfigViewModel model)
        {
            if (ModelState.IsValid)
            {
                ClientProxyBeerConfig.Execute(a => a.AddMarketConfig(UserPass, model.Entity));
            }
            return RedirectToAction("Shops", "References");
        }

        [HttpGet]
        public ActionResult EditShopConfig(int shopId)
        {
            var model = new ShopConfigViewModel
            {
                Entity = ClientProxyBeerConfig.Execute(a => a.GetMarketDetailConfig(UserPass, shopId)),
                Drinks = _repository.ClientProxyBeerData.Execute(a => a.GetDrinks(UserPass)).ToArray()
            };
            
            if (model.Entity.ActualDate == DateTimeOffset.MinValue)
                model.Entity.ActualDate = DateTime.Now.AddDays(1);

            return View(model);

        }

        [HttpPost]
        public ActionResult EditShopConfig(ShopConfigViewModel model)
        {
            if (ModelState.IsValid && model != null)
            {
                model.Entity.Drinks =
                    ClientProxyBeerConfig.Execute(a => a.GetMarketDetailConfig(UserPass, model.Entity.Id)).Drinks;

                if (model.JsonDrinkSettings != null)
                {
                    var settings = JsonConvert.DeserializeObject<Dictionary<int, int>>(model.JsonDrinkSettings);
                    var futureDrinks = _repository.ClientProxyBeerData.Execute(a => a.GetDrinks(UserPass));

                    foreach (var setting in settings)
                    {
                        var newDrink = model.Entity.Drinks.FirstOrDefault(a => a.TapCode == setting.Key);

                        if (newDrink != null)
                            newDrink.FutureDrink = futureDrinks.FirstOrDefault(a => a.Id == setting.Value);
                    }
                }


                ClientProxyBeerConfig.Execute(a => a.SetMarketDetailConfig(UserPass, model.Entity.Id, model.Entity));
            }
            return RedirectToAction("Shops", "References");
        }

        [HttpPost]
        public ActionResult EditMarketDrink(int tapCode, int idMarket, int idNewDrink)
        {
            var item = ClientProxyBeerConfig.Execute(a => a.GetMarketDetailConfig(UserPass, idMarket));
            var futureDrink = _repository.ClientProxyBeerData.Execute(a => a.GetDrinks(UserPass)).FirstOrDefault(a => a.Id == idNewDrink);

            var curDrink = item.Drinks.FirstOrDefault(a => a.TapCode == tapCode);
            if (curDrink != null)
                curDrink.FutureDrink = futureDrink;

            ClientProxyBeerConfig.Execute(a => a.SetMarketDetailConfig(UserPass, idMarket, item));

            return null;
        }
    }
}