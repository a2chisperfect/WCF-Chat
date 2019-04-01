using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Server.Model
{
    [DataContract]
    public class Message
    {
        [DataMember]
        public UserInfo Sender { get; set; }
        [DataMember]
        public UserInfo Reciver { get; set; }
        [DataMember]
        public string message { get; set; }
        [DataMember]
        public DateTime Time { get; set; }
    }
}
