using System.Collections.Generic;

namespace Contracts.Retail.Domain
{
    /// <summary>
    /// Стойка, содержит одну или несколько полок с товаром
    /// </summary>
    public class Rack : UniqueId
    {
        public virtual List<Shelf> Shelves { get; set; }
    }
}