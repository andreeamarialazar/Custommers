using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;
namespace Manager {
    [RunInstaller(true)] 
    public class CustommerServiceInstaller : Installer
    { private ServiceProcessInstaller process; 
        private ServiceInstaller service;
        public CustommerServiceInstaller() { 
            process = new ServiceProcessInstaller(); 
            process.Account = ServiceAccount.LocalSystem; 
            service = new ServiceInstaller(); 
            service.ServiceName = "CustommerService"; 
            Installers.Add(process); 
            Installers.Add(service); } }
}