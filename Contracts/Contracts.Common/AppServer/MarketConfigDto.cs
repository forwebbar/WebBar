using System.Runtime.Serialization;

namespace Contracts.Common.AppServer
{
    /// <summary>
    /// Транспортный объект настроек магазина
    /// </summary>
    [DataContract]
    public class MarketConfigDto
    {
        /// <summary>
        /// Идентификатор магазина
        /// </summary>
        [DataMember]
        public int Id { get; set; }

        /// <summary>
        /// Содержимое колонки ID
        /// </summary>
        [DataMember]
        public string Code { get; set; }

        /// <summary>
        /// Наименование магазина
        /// </summary>
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// Адрес магазина
        /// </summary>
        [DataMember]
        public string Address { get; set; }

    }
}