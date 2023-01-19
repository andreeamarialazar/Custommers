using Resource.DataContracts;
using Resource.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using Utilities;
namespace Resource.Services
{
    public class CustommerResource : ICustommerResource

    {
        private DbObjects.CustommersModelsDataContext dbContext;
        public CustommerResource()
        {
            this.dbContext = new DbObjects.CustommersModelsDataContext();
        }
        public CustommerResource(DbObjects.CustommersModelsDataContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public List<Custommer> GetAllCustommers()
        {
            List<Custommer> members = new List<Custommer>();
            foreach (DbObjects.Custommer custommer in dbContext.Custommers)
            {
                members.Add(custommer.MapObject<Custommer>());
            }
            return members;
        }
        public Custommer GetCustommerByID(Guid ID)
        {
            DbObjects.Custommer existingCustommer = dbContext.Custommers.FirstOrDefault(x => x.IDCustommer == ID);
            if (existingCustommer != null)
                return existingCustommer.MapObject<Custommer>();
            else
                return null;
        }
        public void InsertCustommer(Custommer custommer)
        {
            custommer.IDCustommer = Guid.NewGuid();
            dbContext.Custommers.InsertOnSubmit(custommer.MapObject < DbObjects.Custommer > ());
            dbContext.SubmitChanges();
        }
        public void UpdateCustommer(Custommer custommer)
        {
            DbObjects.Custommer existingmember = dbContext.Custommers.FirstOrDefault(x => x.IDCustommer == custommer.IDCustommer);
            if (existingmember != null)
            {
                existingmember.UpdateObject(custommer);
                dbContext.SubmitChanges();
            }
        }
        public void DeleteCustommer(Guid ID)
        {
            DbObjects.Custommer cutommerToDelete = dbContext.Custommers.FirstOrDefault(x => x.IDCustommer == ID);
            if (cutommerToDelete != null)
            {
                dbContext.Custommers.DeleteOnSubmit(cutommerToDelete);
                dbContext.SubmitChanges();
            }
        }
    }
}