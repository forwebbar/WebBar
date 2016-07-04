using System.Configuration;
using System.ServiceModel;
using System.ServiceProcess;
using Common.Logging;
using Impl.Core;
using WebBar.BeerServer.Config;
using WebBar.BeerServer.Data;
using WebBar.BeerServer.DataCollector;

namespace WebBar.BeerServer
{
    internal class BeerServerMain : ServiceBase, IStart
    {
        private readonly ILog _logger = LogManager.GetLogger<BeerServerMain>();
        private ServiceHost _service;
        private ServiceHost _configService;
        private BackGroundProcess _backGroundProcess;
        private IExceutable _collector;


        public void Start()
        {
            OnStart(new string[] { });
        }

        /// <summary>
        /// Обработчик запуска службы
        /// Этот метод считать точкой входа а-ля main()
        /// </summary>
        /// <param name="args"></param>
        protected override void OnStart(string[] args)
        {
            _service = new ServiceHost(typeof(BeerDataService));
            _service.Open();            

            _configService = new ServiceHost(typeof(BeerConfigurationService));
            _configService.Open();

            string folder = ConfigurationManager.AppSettings["BaseFolder"];

            int cycleLimit;
            if (!int.TryParse(ConfigurationManager.AppSettings["CycleLimit"], out cycleLimit))
                cycleLimit = 10;

            //_collector = new FileCollector(folder, cycleLimit);
            //_backGroundProcess = new BackGroundProcess(_collector);
            //_backGroundProcess.Start();

            _logger.Info("OnStart");
        }

        /// <summary>
        /// Обработчик остановки службы
        /// </summary>
        protected override void OnStop()
        {
            _service.Close();
            _configService.Close();

            _logger.Info("OnStop");
        }
    }
}