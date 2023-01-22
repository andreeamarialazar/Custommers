using System;
using System.Collections.Generic;
using System.ServiceModel; //comunicarea cu dintre servicii
using Newtonsoft.Json; // serializare si deserializare de date
using Utilities; // object mapping of values
//fiecare serviciu este recomandat sa aiba subnamespace separat
namespace EngineProxy
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

    public class CustommerName
    {
        

        public Guid ID { get; set; }
       
        public string Name { get; set; }
    }
    #endregion Service Models

    public class CustommerEngineProxy
    {
        private readonly string serviceBaseAddress = "net.tcp://localhost:8550/CustommerEngineService/";
        private readonly NetTcpBinding binding = new NetTcpBinding();
        private readonly ChannelFactory<Engine.ServiceContracts.ICustommerValidationService> channelFactory;
        private Engine.ServiceContracts.ICustommerValidationService client = null;
        public CustommerEngineProxy()
        {
            this.channelFactory = new
           ChannelFactory<Engine.ServiceContracts.ICustommerValidationService>(binding, serviceBaseAddress);
        }
        #region ChannelHelperMethods
        private void InitializeCustommerServiceClient()
        {
            if (this.client != null && this.client.GetType() ==
           typeof(Engine.ServiceContracts.ICustommerValidationService))
                return;
            Engine.ServiceContracts.ICustommerValidationService client = null;
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
           // ((ICommunicationObject)client).Close();
            channelFactory.Close();
        }

        #endregion ChannelHelperMethods
        #region ServiceProxyMethods
        public Error[] ValidateName(CustommerName custommerName)
        {
            //reference: https://stackoverflow.com/questions/2943148/how-toprogrammatically-connect-a-client-to-a-wcf-service
            try
            {
                InitializeCustommerServiceClient();
                string jsonResult =
               JsonConvert.SerializeObject(client.ValidateName(custommerName.MapObject<Engine.DataContracts.CustommerName>()));
                Error[] errors =
               JsonConvert.DeserializeObject<Error[]>(jsonResult);
               // CloseChannel();
                return errors;
            }
            catch (Exception exception)
            {
                Abort();
                throw exception;
            }
        }

        public Error[] Validate(Custommer custommer)
        {
            //reference: https://stackoverflow.com/questions/2943148/how-toprogrammatically-connect-a-client-to-a-wcf-service
            try
            {
                InitializeCustommerServiceClient();
                string jsonResult =
               JsonConvert.SerializeObject(client.Validate(custommer.MapObject<Engine.DataContracts.Custommer>()));
                Error[] errors =
               JsonConvert.DeserializeObject<Error[]>(jsonResult);
               // CloseChannel();
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
