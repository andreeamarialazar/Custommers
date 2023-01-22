using System;
using System.ServiceModel; //comunicarea cu dintre servicii
using Newtonsoft.Json; // serializare si deserializare de date
using Utilities; // object mapping of values
//fiecare serviciu este recomandat sa aiba subnamespace separat
namespace ServiceProxy
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
    public class CustommerServiceProxy
    {
        private readonly string serviceBaseAddress ="net.tcp://localhost:8510/CustommerService/";
        private readonly NetTcpBinding binding = new NetTcpBinding();
        private readonly ChannelFactory<Manager.ServiceContracts.ICustommerService>
       channelFactory;
        private Manager.ServiceContracts.ICustommerService client = null;
        public CustommerServiceProxy()
        {
            this.channelFactory = new
           ChannelFactory<Manager.ServiceContracts.ICustommerService>(binding,
           serviceBaseAddress);
        }
        #region ChannelHelperMethods
        private void InitializeCustommerServiceClient()
        {
            if (this.client != null && this.client.GetType() ==
           typeof(Manager.ServiceContracts.ICustommerService))
                return;
            Manager.ServiceContracts.ICustommerService client = null;
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
 public Custommer[] GetCustommers()
        {
            //reference: https://stackoverflow.com/questions/2943148/how-toprogrammatically-connect-a-client-to-a-wcf-service
            try
            {
                InitializeCustommerServiceClient();
                string jsonResult =
               JsonConvert.SerializeObject(client.GetCustommers());
                Custommer[] custommers =
               JsonConvert.DeserializeObject<Custommer[]>(jsonResult);
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
        public Error[] AddCustommer(Custommer custommer)
        {
            try
            {
                InitializeCustommerServiceClient();
                string jsonResult =
               JsonConvert.SerializeObject(client.AddCustommer(custommer.MapObject < Manager.DataContracts.Custommer > ()));
                Error[] errors = JsonConvert.DeserializeObject<Error[]>(jsonResult);
                CloseChannel();
                return errors;
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
