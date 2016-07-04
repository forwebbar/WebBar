using System;
using System.ServiceModel;
using System.Web;
using System.Web.Mvc;
using Contracts.Common;
using Contracts.Common.AppServer;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using Ninject;
using Ninject.Web.Common;
using Ninject.Web.Mvc;
using WebBar.Site.Controllers;
using WebBar.Site.ServiceClients;

namespace WebBar.Site.CompositionRoot
{
    public class DependencyInjectionConfig
    {
        #region Fields

        private static Bootstrapper _ninjectBootstrapper;
        private static IKernel _kernel;
        private static bool _isInitialized;
        private static bool _isStopped;

        #endregion

        #region Methods

        #region Dependency injection initialization (Ninject)
        public static void Start()
        {
            if (_isInitialized)
                return;
            _ninjectBootstrapper = new Bootstrapper();
            DynamicModuleUtility.RegisterModule(typeof (OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof (NinjectHttpModule));
            _ninjectBootstrapper.Initialize(CreateKernel);

            _isInitialized = true;
        }

        public static void Stop()
        {
            if (_isStopped)
                return;

            _ninjectBootstrapper.ShutDown();
            _isStopped = true;
        }

        private static IKernel CreateKernel()
        {
            if (_kernel != null) return _kernel;

            _kernel = new StandardKernel();
            try
            {
                var kernel = _kernel;
                _kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => kernel);
                _kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(_kernel);

                return _kernel;
            }
            catch
            {
                _kernel.Dispose();
                throw;
            }
        }

        private static void RegisterServices(IKernel kernel)
        {
            DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));

            kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();
            kernel.Bind<IAuthorize>().To<AuthorizeService>();
            
            kernel.Bind<ChannelFactory<IAuthorize>>().To<ChannelFactory<IAuthorize>>().WithConstructorArgument("AuthorizeServiceEndpoint");
            kernel.Bind<ChannelFactory<IBeerData>>().To<ChannelFactory<IBeerData>>().WithConstructorArgument("NetTcpBinding_IBeerData");
            kernel.Bind<ChannelFactory<IBeerConfig>>().To<ChannelFactory<IBeerConfig>>().WithConstructorArgument("NetTcpBinding_IBeerConfig");
        }

        #endregion
        
        
        #endregion
    }
}