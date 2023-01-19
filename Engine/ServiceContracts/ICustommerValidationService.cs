using Engine.DataContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Engine.ServiceContracts
{
    [ServiceContract] 
    public interface ICustommerValidationService {
        [OperationContract] 
        Error[] ValidateName(CustommerName custommerName);
        [OperationContract] 
        Error[] Validate(Custommer custommer); }
}
