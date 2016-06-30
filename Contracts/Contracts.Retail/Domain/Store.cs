using System.Collections.Generic;

namespace Contracts.Retail.Domain
{
    /// <summary>
    /// Магазин, содержит один и более холодильников
    /// </summary>
    public class Store : UniqueId
    {
        public virtual List<Rack> Racks { get; set; }
    }
}
