using Contracts.Common;
using Contracts.Common.AppServer;

namespace WebBar.BeerServer.Config
{
    internal interface IConfigProviderFactory
    {
        IBeerConfig GetOrCreateProvider(UserPass user);
    }
}