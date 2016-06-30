namespace Contracts.Retail.Domain
{
    /// <summary>
    /// Наслденик этого класса имеет уникальный иденификатор в системе
    /// </summary>
    public abstract class UniqueId
    {
        /// <summary>
        /// Уникальный идентификатор
        /// </summary>
        public long Id { get; set; }
    }
}