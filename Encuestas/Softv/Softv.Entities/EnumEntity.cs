using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Softv.Entities
{
    [DataContract]
    [DataObject]
    [Serializable]
    public class EnumEntity
    {
        [DataMember]
        public int? Id { get; set; }
        /// <summary>
        /// Property Nombre
        /// </summary>

        [DataMember]
        public String Nombre { get; set; }
    }
}
