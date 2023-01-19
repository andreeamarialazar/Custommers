using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;
namespace Resource
{
    [RunInstaller(true)]
    public class BlogRepositoryServiceProjectInstaller : Installer
    {
        private ServiceProcessInstaller process;
        private ServiceInstaller service;
        public BlogRepositoryServiceProjectInstaller()
        {
            process = new ServiceProcessInstaller();
            process.Account = ServiceAccount.LocalSystem;
            service = new ServiceInstaller();
            service.ServiceName = "DataServices";
            Installers.Add(process);
            Installers.Add(service);
        }
    }
}