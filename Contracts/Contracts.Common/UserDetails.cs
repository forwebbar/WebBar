using System.Runtime.Serialization;

namespace Contracts.Common
{
    [DataContract]
    public class UserDetails
    {
        [DataMember]
        public string Username { get; set; }

        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public string Password { get; set; }

        [DataMember]
        public byte[] Code { get; set; }

    }
}