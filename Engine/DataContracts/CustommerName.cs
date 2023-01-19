using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Engine.DataContracts
{
    [DataContract] 
    public class CustommerName 
    {[DataMember] 
        
       public Guid ID { get; set; }
        [DataMember] 
        public string Name { get; set; } }
}
