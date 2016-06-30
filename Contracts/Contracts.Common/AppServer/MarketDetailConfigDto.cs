using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Contracts.Common.AppServer
{
    [DataContract]
    public class MarketDetailConfigDto
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

        /// <summary>
        /// Содержимое колонки "Контроллер №"
        /// </summary>
        [DataMember]
        public string DeviceSerial { get; set; }

        /// <summary>
        /// Дата начала действия изменений
        /// </summary>
        [DataMember]
        public DateTimeOffset ActualDate { get; set; }

        /// <summary>
        /// Таблица напитков
        /// </summary>
        [DataMember]
        public List<MarketDrinkListItemDto> Drinks { get; set; }
    }
}