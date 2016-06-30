using System;
using Contracts.Common.AppServer;

namespace WebBar.Site.Models
{
    public class PriceViewModel
    {
        public PriceDto PriceDto { get; set; }
        public MarketDto MarketDto { get; set; }
        public DateTime DrinkPriceStart { get; set; }
        public string NewPrice { get; set; }
    }
}