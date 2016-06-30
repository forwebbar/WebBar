using System.ServiceModel;
using System.ServiceProcess;
using Common.Logging;
using Contracts.Common;
using Impl.Core;
using Impl.Server;

namespace WebBar.AuthorizeServer
{
    internal class AuthorizeMain : ServiceBase, IStart
    {
        private readonly ILog _logger = LogManager.GetLogger<AuthorizeMain>();
        private ServiceHost _service;
        private IAuthorize _instance;

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
            _instance = new AuthorizationService();
            _service = new ServiceHost(_instance);
            _service.Open();
            _logger.Info("OnStart");
        }

        /// <summary>
        /// Обработчик остановки службы
        /// </summary>
        protected override void OnStop()
        {
            _service.Close();
            _logger.Info("OnStop");
        }
    }
}