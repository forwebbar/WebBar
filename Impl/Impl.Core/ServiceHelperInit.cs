using System;
using System.IO;
using System.Reflection;
using System.ServiceProcess;

namespace Impl.Core
{
    public static class ServiceHelperInit
    {
        public static void InitService<T>(string[] args, T service)
            where T : ServiceBase, IStart
        {
            if (args == null || args.Length <= 1)
            {
                StartService(service);
                return;
            }

            var serviceNameArg = string.Empty;
            if (args.Length > 1)
                serviceNameArg = args[1];

            var customArg = string.Empty; 
            if (args.Length > 2)
                customArg = args[2];

            var param = args[0];
            if (param.StartsWith("\""))
                param = param.Substring(1);

            if (param.EndsWith("\""))
                param = param.Substring(0, param.Length - 1);

            if (!param.StartsWith("/"))
            {
                ShowUsage();
                return;
            }
            param = param.Substring(1);

            var serviceName = string.IsNullOrEmpty(serviceNameArg) ? service.ServiceName : serviceNameArg;
            switch (param.ToLower())
            {
                case "i":
                    ServiceHelperInstall.InstallService(serviceName, customArg);
                    break;
                case "u":
                    ServiceHelperInstall.UninstallService(serviceName);
                    break;
                default:
                    var oldColor = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Unknown parameter: " + args[0]);
                    Console.ForegroundColor = oldColor;
                    Console.WriteLine();
                    ShowUsage();
                    return;
            }
        }

        public static void StartService<T>(T service)
            where T : ServiceBase, IStart
        {
            // Использование сервиса в консоли
            var servicesToRun = new ServiceBase[] { service };

            if (Environment.UserInteractive)
            {
                Console.CancelKeyPress += (x, y) => service.Stop();
                service.Start();
                Console.WriteLine("Running service, press any key to stop.");
                Console.ReadKey();
                service.Stop();
                Console.WriteLine("Service stopped. Goodbye.");
            }
            else
            {
                ServiceBase.Run(servicesToRun);
            }
        }

        private static void ShowUsage()
        {
            var oldColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Use: {0} [params]", Path.GetFileName(Assembly.GetExecutingAssembly().Location));
            Console.WriteLine("where params:");
            Console.WriteLine("\t/i [ServiceName] [StartUpArgument] - Install service. StartUpArgument must be in \"key=value\" format");
            Console.WriteLine("\t/u [ServiceName] - UnInstall service");
            Console.ForegroundColor = oldColor;
        }
    }
    
}
