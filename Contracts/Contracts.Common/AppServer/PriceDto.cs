using System;
using System.Runtime.Serialization;

namespace Contracts.Common.AppServer
{
    /// <summary>
    /// Транспортный объект цены напитка
    /// </summary>
    [DataContract]
    public class PriceDto
    {
        /// <summary>
        /// Идентификатор цены
        /// </summary>
        [DataMember]
        public int Id { get; set; }

        /// <summary>
        /// Текущая цена в копейках
        /// </summary>
        [DataMember]
        public int CurrentVal { get; set; }

        /// <summary>
        /// Будущая цена в копецках
        /// </summary>
        [DataMember]
        public int FuturePrice { get; set; }

        /// <summary>
        /// Дата начала действия будущей цены
        /// </summary>
        [DataMember]
        public DateTimeOffset FutureDate { get; set; }
    }
}