using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class CustommerEngineService : ServiceBase
    {
        public ServiceHost serviceHost = null; public CustommerEngineService()
        { // Name the Windows Service
          ServiceName = "CustommerEngineService"; } public static void Main() { ServiceBase.Run(new CustommerEngineService()); } 
        // Start the Windows service.
        protected override void OnStart(string[] args)
        { if (serviceHost != null) 
            { serviceHost.Close(); } serviceHost = new ServiceHost(typeof(Engine.Services.CustommerValidationService));
          
// Open the ServiceHostBase to create listeners and start
// listening for messages.
serviceHost.Open(); } protected override void OnStop() { if (serviceHost != null) { serviceHost.Close(); serviceHost = null; } } }
}
