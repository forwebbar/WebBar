using System.Runtime.Serialization;

namespace Contracts.Common.AppServer
{
    /// <summary>
    /// Транспортный объект настроек сорта
    /// </summary>
    [DataContract]
    public class DrinkConfigDto
    {
        /// <summary>
        /// Идентификатор сорта
        /// </summary>
        [DataMember]
        public int Id { get; set; }

        /// <summary>
        /// Значение колонки ID
        /// </summary>
        [DataMember]
        public string Code { get; set; }

        /// <summary>
        /// Наименование
        /// </summary>
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// Идентификатор поставщика
        /// </summary>
        [DataMember]
        public int IdProducer { get; set; }

        /// <summary>
        /// Значение колонки "Поставщик"
        /// </summary>
        [DataMember]
        public string ProducerName { get; set; }
    }
}