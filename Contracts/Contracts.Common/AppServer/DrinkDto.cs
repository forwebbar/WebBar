using System.Runtime.Serialization;

namespace Contracts.Common.AppServer
{
    [DataContract]
    public class DrinkDto
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Name { get; set; }
    }
}