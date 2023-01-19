using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace Manager
{
    public class CustommerService: ServiceBase

    {
        public ServiceHost serviceHost = null; 
        public CustommerService()
        { // Name the Windows Service
          ServiceName = "CustommerService"; } 
        public static void Main() { 
            ServiceBase.Run(new CustommerService()); } 
        // Start the Windows service.
        protected override void OnStart(string[] args) { 
            if (serviceHost != null) { 
                serviceHost.Close(); } 
            serviceHost = new ServiceHost(typeof(Manager.Services.CustommerService)); 
            // Open the ServiceHostBase to create listeners and start // listening for messages.
            serviceHost.Open(); } 
        protected override void OnStop() { 
            if (serviceHost != null) { 
                serviceHost.Close(); serviceHost = null;
            } } } }

