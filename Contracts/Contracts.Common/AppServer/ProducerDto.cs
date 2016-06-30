using System;
using System.Runtime.Serialization;

namespace Contracts.Common.AppServer
{
    /// <summary>
    /// Трарспортный объект поставщика
    /// </summary>
    [DataContract]
    public class ProducerDto
    {
        /// <summary>
        /// Идентификатор поставщика
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
        /// ИНН
        /// </summary>
        [DataMember]
        public string Inn { get; set; }

        /// <summary>
        /// КПП
        /// </summary>
        [DataMember]
        public string Kpp { get; set; }

        /// <summary>
        /// ОГРН(ИП)
        /// </summary>
        [DataMember]
        public string Ogrn { get; set; }

        /// <summary>
        /// Р/С
        /// </summary>
        [DataMember]
        public string Account { get; set; }

        /// <summary>
        /// БИК
        /// </summary>
        [DataMember]
        public string Bik { get; set; }

        /// <summary>
        /// Банк
        /// </summary>
        [DataMember]
        public string Bank { get; set; }

        /// <summary>
        /// Дата
        /// </summary>
        [DataMember]
        public DateTimeOffset? ActualDate { get; set; }

        /// <summary>
        /// Юридический адресс
        /// </summary>
        [DataMember]
        public string LawAdress { get; set; }

        /// <summary>
        /// Фактический адресс
        /// </summary>
        [DataMember]
        public string ActualAdress { get; set; }
    }
}