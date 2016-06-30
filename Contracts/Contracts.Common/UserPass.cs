using System;
using System.Runtime.Serialization;

namespace Contracts.Common
{
    [DataContract]
    public class UserPass
    {
        [DataMember]
        public string Username { get; set; }

        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public Guid UId { get; set; }
    }
}