using Resource.Services;
using System.ServiceModel;
using System.ServiceProcess;
namespace Resource
{
    public class DataServices : ServiceBase
    {
        public ServiceHost serviceHost = null;
        public DataServices()
        {
            // Name the Windows Service
            ServiceName = "DataServices";
        }
        public static void Main()
        {
            ServiceBase.Run(new DataServices());
        }
        // Start the Windows service.
        protected override void OnStart(string[] args)
        {
            if (serviceHost != null)
            {
                serviceHost.Close();
            }

            serviceHost = new ServiceHost(typeof(CustommerResource));
            // Open the ServiceHostBase to create listeners and start
            // listening for messages.
            serviceHost.Open();
        }
        protected override void OnStop()
        {
            if (serviceHost != null)
            {
                serviceHost.Close();
                serviceHost = null;
            }
        }
    }
}
