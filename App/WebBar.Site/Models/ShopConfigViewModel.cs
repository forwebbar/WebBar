using Contracts.Common.AppServer;

namespace WebBar.Site.Models
{
    public class ShopConfigViewModel
    {
        public MarketDetailConfigDto Entity { get; set; }
        public DrinkDto[] Drinks { get; set; }

        public string JsonDrinkSettings { get; set; }
    }
}