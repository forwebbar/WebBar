using System;
using Contracts.Common.AppServer;

namespace WebBar.Site.Models
{
    public class EditDrinksConfigViewModel
    {
        public DrinkConfigDto EditEntity { get; set; }
        public ProducerDto[] Producers { get; set; }
        public DateTime DrinkPriceStart { get; set; }
        public string OnePrice { get; set; }
        public PriceViewModel[] Prices { get; set; }
    }
}