using System;
using System.Collections;
using System.Configuration.Install;
using System.IO;
using System.Reflection;
using System.ServiceProcess;

namespace Impl.Core
{
    static class ServiceHelperInstall
    {
        private static readonly string ExecutablePath = (Assembly.GetEntryAssembly() ?? Assembly.GetExecutingAssembly()).Location;

        private class HostServiceInstaller : Installer
        {
            private readonly string _serviceName;

            public HostServiceInstaller(string serviceName)
            {
                if (string.IsNullOrEmpty(serviceName))
                    throw new ArgumentException("Invalid service name.", "serviceName");

                _serviceName = serviceName;
                var serviceInstaller = new ServiceInstaller
                {
                    StartType = ServiceStartMode.Automatic,
                    ServiceName = _serviceName,
                    DisplayName = _serviceName
                };
                Installers.Add(serviceInstaller);

                var processInstaller = new ServiceProcessInstaller
                {
                    Account = ServiceAccount.LocalSystem
                };
                Installers.Add(processInstaller);
            }

            public override void Uninstall(IDictionary stateSaver)
            {
                var controller = new ServiceController(_serviceName, Environment.MachineName);
                if (controller.CanStop)
                    controller.Stop();
                controller.Close();
                base.Uninstall(stateSaver);
            }
        }

        public static void InstallService(string serviceName, string customArg)
        {
            try
            {
                var savedState = new Hashtable();

                var installContext = new InstallContext(Path.ChangeExtension(ExecutablePath, ".InstallLog"), new[] { string.Empty });
                installContext.Parameters["assemblypath"] = string.IsNullOrEmpty(customArg) ? ExecutablePath :
                                                                                                  ExecutablePath + " \"" + customArg + "\"";

                var hostServiceInstaller = new HostServiceInstaller(serviceName) { Context = installContext };
                hostServiceInstaller.Install(savedState);
                hostServiceInstaller.Commit(savedState);
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Service install failed:");
                Console.WriteLine(ex);
                Console.ResetColor();
            }
        }

        public static void UninstallService(string serviceName)
        {
            try
            {
                var installContext = new InstallContext(Path.ChangeExtension(ExecutablePath, ".InstallLog"), new[] { string.Empty });
                installContext.Parameters["assemblypath"] = ExecutablePath;

                var hostServiceInstaller = new HostServiceInstaller(serviceName) { Context = installContext };
                hostServiceInstaller.Uninstall(null);
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Service uninstall failed:");
                Console.WriteLine(ex);
                Console.ResetColor();
            }
        }
    }
}