using Engine.DataContracts;
using Engine.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Services
{
    public class CustommerValidationService : ICustommerValidationService
    {
        private readonly CustommerResource.CustommerResourceClient custommerResource; public CustommerValidationService()
        {
            this.custommerResource = new CustommerResource.CustommerResourceClient();
           
}
        public Error[] ValidateName(CustommerName custommerName) { List<Error> errors = new List<Error>(); CustommerResource.Custommer[] existingCustommers = custommerResource.GetAllCustommers(); if (existingCustommers.Any(x => x.Name == custommerName.Name && x.IDCustommer != custommerName.ID)) errors.Add(new Error { ErrorCode = "V1", Message = "Duplicate name" }); return errors.ToArray(); }
        public Error[] Validate(Custommer custommer)
        {
            List<Error> errors = new List<Error>(); 
            if (string.IsNullOrEmpty(custommer.Name)) errors.Add(new Error { ErrorCode = "V2.1", Message = "Name cannot be empty" }); if (string.IsNullOrEmpty(custommer.Title)) errors.Add(new Error { ErrorCode = "V2.2", Message = "Title cannot be empty" }); if (string.IsNullOrEmpty(custommer.Position)) errors.Add(new Error { ErrorCode = "V2.3", Message = "Position cannot be empty" }); if (errors.Any()) return errors.ToArray(); if (custommer.Name.Length < 3) errors.Add(new Error { ErrorCode = "V2.4", Message = "Name is too short" }); if (custommer.Name.Length > 250) errors.Add(new Error { ErrorCode = "V2.5", Message = "Name is too long" }); if (custommer.Title.Length < 2) errors.Add(new Error { ErrorCode = "V2.6", Message = "Title is too short" }); if (custommer.Title.Length > 250) errors.Add(new Error { ErrorCode = "V2.7", Message = "Title is too long" }); if (custommer.Position.Length < 2)
                
            errors.Add(new Error { ErrorCode = "V2.8", Message = "Position is too short" }); 
            if (custommer.Position.Length > 250) errors.Add(new Error { ErrorCode = "V2.9", Message = "Position is too long" }); 
            if (custommer.Description.Length > 1000) errors.Add(new Error { ErrorCode = "V2.10", Message = "Description is too long" }); 
            return errors.ToArray();
        }
    }
}
