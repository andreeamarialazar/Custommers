using Resource.DataContracts;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace Resource.ServiceContracts
{
    [ServiceContract]
    public interface ICustommerResource
    {
        [OperationContract]
        List<Custommer> GetAllCustommers();
        [OperationContract]
        Custommer GetCustommerByID(Guid ID);
        [OperationContract]
        void InsertCustommer(Custommer custommer);
        [OperationContract]
        void UpdateCustommer(Custommer custommer);
        [OperationContract]
        void DeleteCustommer(Guid ID);
    }
}
