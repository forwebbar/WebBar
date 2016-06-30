using System;
using System.Runtime.Serialization;

namespace Contracts.Common.AppServer
{
    /// <summary>
    /// Транспортный объект таблицы напитков
    /// </summary>
    [DataContract]
    public class MarketDrinkListItemDto
    {
        /// <summary>
        /// Содержимое колонки "Кран"
        /// </summary>
        [DataMember]
        public int TapCode { get; set; }

        /// <summary>
        /// Колонка "Текущий сорт"
        /// </summary>
        [DataMember]
        public DrinkDto CurDrink { get; set; }

        /// <summary>
        /// Будущий сорт
        /// </summary>
        [DataMember]
        public DrinkDto FutureDrink { get; set; }

        /// <summary>
        /// Будущий сорт "дата с"
        /// </summary>
        [DataMember]
        public DateTimeOffset FutureDrinkDate { get; set; }

        /// <summary>
        /// Новый сорт
        /// </summary>
        [DataMember]
        public DrinkDto NewDrink { get; set; }
    }
}