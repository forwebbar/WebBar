namespace Contracts.Retail.Domain
{
    /// <summary>
    /// Товар
    /// </summary>
    public class Product : UniqueId
    {
        /// <summary>
        /// Масса продукта
        /// </summary>
        public int Mass { get; }
        /// <summary>
        /// Наименование
        /// </summary>
        public string Name { get; }
        /// <summary>
        /// Цена товара
        /// </summary>
        public long Price { get; }
    }
}