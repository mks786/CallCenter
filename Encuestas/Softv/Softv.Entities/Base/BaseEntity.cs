using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Softv.Entities
{
    [DataContract]
    [Serializable]
    public abstract class BaseEntity
    {
        [DataMember]
        public int BaseIdUser { get; set; }
        [DataMember]
        public String BaseRemoteIp { get; set; }
    }
}
