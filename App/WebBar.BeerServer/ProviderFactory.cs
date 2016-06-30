using Contracts.Common;
using Contracts.Common.AppServer;
using WebBar.BeerServer.Config;
using WebBar.BeerServer.Data;
using WebBar.BeerServer.Security;

namespace WebBar.BeerServer
{
    internal class ProviderFactory : IDataProviderFactory, IConfigProviderFactory
    {
        public IBeerData GetOrCreateProvider(UserPass user)
        {
            //return new FakeProvider();
            return new DbProvider(new FakeSecurityProvider());
        }

        IBeerConfig IConfigProviderFactory.GetOrCreateProvider(UserPass user)
        {
            return new ConfigProvider(new FakeSecurityProvider());
        }
    }
}