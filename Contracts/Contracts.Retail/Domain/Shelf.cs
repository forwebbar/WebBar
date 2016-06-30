using System.Collections.Generic;

namespace Contracts.Retail.Domain
{
    /// <summary>
    /// Полка шкафа, на ней стоит товар
    /// </summary>
    public class Shelf : UniqueId
    {
        /// <summary>
        /// Текущее кол-во товара на полке по типам товара
        /// </summary>
        public Dictionary<Product, int> CurrentProducts { get; set; }

        /// <summary>
        /// Максимальное эталонное количество товара на полке по типам товара
        /// </summary>
        public Dictionary<Product, int> MaxProducts { get; set; } 
    }
}