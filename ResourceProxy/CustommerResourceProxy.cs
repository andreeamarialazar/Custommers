using System;
using System.Collections.Generic;
using System.ServiceModel; //comunicarea cu dintre servicii
using Newtonsoft.Json; // serializare si deserializare de date
using Utilities; // object mapping of values
//fiecare serviciu este recomandat sa aiba subnamespace separat
namespace ResourceProxy
{
    #region ServiceModels
    public class Custommer
    {
        public Guid IDCustommer { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Position { get; set; }
        public string Description { get; set; }
        public string Resume { get; set; }
    }

    public class Error
    {
        public string ErrorCode { get; set; }
        public string Message { get; set; }
    }
    #endregion Service Models

    public class CustommerResourceProxy
    {
        private readonly string serviceBaseAddress = "net.tcp://localhost:8523/DataServices/";
        private readonly NetTcpBinding binding = new NetTcpBinding();
        private readonly ChannelFactory<Resource.ServiceContracts.ICustommerResource>
       channelFactory;
        private Resource.ServiceContracts.ICustommerResource client = null;
        public CustommerResourceProxy()
        {
            this.channelFactory = new
           ChannelFactory<Resource.ServiceContracts.ICustommerResource>(binding, serviceBaseAddress);
        }
        #region ChannelHelperMethods
        private void InitializeCustommerServiceClient()
        {
            if (this.client != null && this.client.GetType() ==
           typeof(Resource.ServiceContracts.ICustommerResource))
                return;
            Resource.ServiceContracts.ICustommerResource client = null;
            try
            {
                this.client = channelFactory.CreateChannel();
            }
            catch (Exception exception)
            {
                (this.client as ICommunicationObject)?.Abort();
                throw exception;
            }
        }
        private void Abort()
        {
            (client as ICommunicationObject)?.Abort();
        }
        private void CloseChannel()
        {
            ((ICommunicationObject)client).Close();
            channelFactory.Close();
        }

        #endregion ChannelHelperMethods
        #region ServiceProxyMethods
        public List<Custommer> GetAllCustommers()
        {
            //reference: https://stackoverflow.com/questions/2943148/how-toprogrammatically-connect-a-client-to-a-wcf-service
            try
            {
                InitializeCustommerServiceClient();
                string jsonResult =
               JsonConvert.SerializeObject(client.GetAllCustommers());
                List<Custommer> custommers =
               JsonConvert.DeserializeObject<List<Custommer>>(jsonResult);
                CloseChannel();
                return custommers;
            }
            catch (Exception exception)
            {
                Abort();
                throw exception;
            }
        }
        public Custommer GetCustommerByID(Guid ID)
        {
            try
            {
                InitializeCustommerServiceClient();
                string jsonResult =
               JsonConvert.SerializeObject(client.GetCustommerByID(ID));
                Custommer custommer =
               JsonConvert.DeserializeObject<Custommer>(jsonResult);
                CloseChannel();
                return custommer;
            }
            catch (Exception exception)
            {
                Abort();
                throw exception;
            }
        }
        public void InsertCustommer(Custommer custommer)
        {
            try
            {
                InitializeCustommerServiceClient();

                client.InsertCustommer(custommer.MapObject<Resource.DataContracts.Custommer>());

                CloseChannel();

            }
            catch (Exception exception)

            {
                Abort();
                throw exception;
            }
        }


        public void UpdateCustommer(Custommer custommer)
        {
            try
            {
                InitializeCustommerServiceClient();

                client.UpdateCustommer(custommer.MapObject<Resource.DataContracts.Custommer>());

                CloseChannel();

            }
            catch (Exception exception)

            {
                Abort();
                throw exception;
            }
        }


        public void DeleteCustommer(Guid ID)
        {
            try
            {
                InitializeCustommerServiceClient();

                client.DeleteCustommer(ID);

               // CloseChannel();

            }
            catch (Exception exception)

            {
                Abort();
                throw exception;
            }
        }
        #endregion
    }
}
