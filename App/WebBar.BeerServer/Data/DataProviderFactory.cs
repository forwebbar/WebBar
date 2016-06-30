using Contracts.Common;
using Contracts.Common.AppServer;

namespace WebBar.BeerServer.Data
{
    internal interface IDataProviderFactory
    {
        IBeerData GetOrCreateProvider(UserPass user);        
    }
}