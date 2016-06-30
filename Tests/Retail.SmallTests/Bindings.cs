using Contracts.Retail.Domain;
using Impl.Client;
using Ninject.Modules;
using Retail.SmallTests.Stubs;

namespace Retail.SmallTests
{
    class Bindings : NinjectModule
    {
        public override void Load()
        {
            Bind<IRepositoryCache>().To<InMemorCache>().InSingletonScope(); 
            Bind<IDataProvider>().To<MockDataProvider>();
            Bind<IConfigurationProvider>().To<MockConfigurationProvider>();
        }
    }
}
