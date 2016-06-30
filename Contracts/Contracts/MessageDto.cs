using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.Serialization;

namespace Contracts
{
    /// <summary>
    /// Транспортный объект сообщения
    /// </summary>
    [DataContract]
    public class MessageDto
    {
        public MessageDto()
        {

        }

        public MessageDto(string subject, string text)
        {
            CreateTags(text);
            ReceivedTimeStamp = DateTime.Now;
            GetTimeStamp();
            GetDeviceCodeAndSerial(subject);
            CheckCrc(text);
        }

        private void CheckCrc(string text)
        {
            int crcPos = text.IndexOf("#CRC16", StringComparison.Ordinal);
            if (crcPos <= 0 || !Tags.ContainsKey("CRC16"))
                throw new Exception("В пиьме нет тега CRC16");

            ushort rCrc = UInt16.Parse(Tags["CRC16"], NumberStyles.AllowHexSpecifier);
            string substring = text.Substring(0, crcPos);
            ushort crc = Crc16.GetCrc16(substring);

            if (rCrc != crc)
                throw new Exception(string.Format("Контрольная сумма не совпала получено {0:X} рассчитано {1:X}",
                    rCrc, crc));
        }

        private void CreateTags(string text)
        {
            Tags = new Dictionary<string, string>();
            var items = text.Split(new[] { '#', '>' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var item in items)
            {
                if (item == "\r\n" || item == "\r")
                    continue;

                var pair = item.Split('=');
                if (pair.Length < 2)
                    continue;

                Tags.Add(pair[0], pair[1]);
            }
        }

        private void GetTimeStamp()
        {
            if (!Tags.ContainsKey("DT"))
                return;

            var dtStr = Tags["DT"];
            var items = dtStr.Split(' ');
            if (items.Length < 3)
                throw new Exception("Неизвестная временная метка");

            if (items[2] != "1" && items[2] != "2")
                throw new Exception("Часы не синхронизированны");

            DateTime ts;
            if (DateTime.TryParse(items[0] + " " + items[1], out ts))
                TimeStamp = ts;
            else
                throw new Exception("Неизвестная временная метка");

            var minDt = new DateTime(2015, 1, 1);
            if (ts < minDt)
                throw new Exception("Временная метка меньше 01.01.2015");

            if (ts > DateTime.Now.AddHours(12))
                throw new Exception("Временная метка более чем на 12 часов превышает временя сервера");
        }

        private void GetDeviceCodeAndSerial(string subject)
        {
            var items = subject.Split(new[] { '<', '>', '=' }, StringSplitOptions.RemoveEmptyEntries);
            if (items.Length < 2)
                throw new Exception("Неверный формат темы письма");

            DeviceCode = items[0];

            var others = items[1].Split(';');
            if (others.Length < 1)
                throw new Exception("Неизвестное устройство");

            SerialNumber = others[0];
        }

        /// <summary>
        /// Тип сообщения
        /// </summary>
        [DataMember]
        public string DeviceCode { get; set; }
        /// <summary>
        /// Источник сообщения
        /// </summary>
        [DataMember]
        public string SerialNumber { get; set; }
        /// <summary>
        /// Временная метка сообщения
        /// </summary>
        [DataMember]
        public DateTime TimeStamp { get; set; }
        /// <summary>
        /// Временная метка получения сообщения
        /// </summary>
        [DataMember]
        public DateTime ReceivedTimeStamp { get; set; }
        /// <summary>
        /// Данные - теги содержащиеся в сообщении
        /// </summary>
        [DataMember]
        public Dictionary<string, string> Tags { get; set; }
    }
}