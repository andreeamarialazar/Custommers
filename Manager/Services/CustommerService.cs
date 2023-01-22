using Manager.DataContracts;
using Manager.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using Utilities;
namespace Manager.Services {
    public class CustommerService : ICustommerService { 
        private readonly ResourceProxy.CustommerResourceProxy custommerResourceProxy;
        private readonly EngineProxy.CustommerEngineProxy custommerValidationProxy;
        public CustommerService()
        {   this.custommerResourceProxy = new ResourceProxy.CustommerResourceProxy();
            this.custommerValidationProxy = new EngineProxy.CustommerEngineProxy();
        }
            
        public Manager.DataContracts.Custommer[] GetCustommers() { 
            //APEL RESURSA
            List<ResourceProxy.Custommer> custommers = custommerResourceProxy.GetAllCustommers(); 
            List<Custommer> custommerList = new List<Custommer>(); 
            //MAPARE OBIECTE DIN CONTRACTUL RESURSEI CATRE CONTRACTUL MANAGERULUI
            foreach (ResourceProxy.Custommer custommer in custommers) 
                custommerList.Add(custommer.MapObject<Manager.DataContracts.Custommer>()); 
            return custommerList.ToArray(); }
        
        
        public Manager.DataContracts.Custommer GetCustommerByID(Guid ID) {
            ResourceProxy.Custommer custommer = custommerResourceProxy.GetCustommerByID(ID);
            return custommer.MapObject<Manager.DataContracts.Custommer>(); } 
        
   //     public void AddCustommer(Custommer custommer) { 
  //          CustommerResource.Custommer newCustommer = custommer.MapObject<CustommerResource.Custommer>();
   //         custommerResource.InsertCustommer(newCustommer); 
        
        
   //     }

        public Manager.DataContracts.Error[] AddCustommer(Custommer custommer)
        {
            ResourceProxy.Custommer newCustommer = custommer.MapObject<ResourceProxy.Custommer>(); 
            EngineProxy.CustommerName custommerName = new EngineProxy.CustommerName { Name = newCustommer.Name }; 
            List<EngineProxy.Error> errors = new List<EngineProxy.Error>(); 
            errors.AddRange(custommerValidationProxy.ValidateName(custommerName)); 
            //validare nume 
            //validare custommer
            errors.AddRange(custommerValidationProxy.Validate(newCustommer.MapObject<EngineProxy.Custommer>())); 
            if (!errors.Any()) custommerResourceProxy.InsertCustommer(newCustommer); 
            //mapare erori pe contractul de manager
            List<Manager.DataContracts.Error> returnedErrors = new List<Manager.DataContracts.Error>(); 
            foreach (EngineProxy.Error error in errors) 
                returnedErrors.Add(error.MapObject<Manager.DataContracts.Error>());
            return returnedErrors.ToArray(); }
        } }