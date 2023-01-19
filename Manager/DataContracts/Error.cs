using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Manager.DataContracts
{
    [DataContract]
    public class Error
    {
        [DataMember] 
        public string ErrorCode { get; set; }
        [DataMember] 
        public string Message { get; set; }

    }
}
