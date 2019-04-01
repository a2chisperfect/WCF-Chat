using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Server.Model
{
    [DataContract]
    public class UserInfo
    {
        [DataMember]
        public Guid Id { get; set; }
        [DataMember]
        public string Name { get; set; }
        public UserInfo(string name)
        {
            Name = name;
            Id = Guid.NewGuid();
        }
    }
}
