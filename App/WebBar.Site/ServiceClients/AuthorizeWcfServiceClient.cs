using System.ServiceModel;
using Contracts.Common;

namespace WebBar.Site.ServiceClients
{
    public class AuthorizeWcfServiceClient : WcfServiceClient<IAuthorize>
    {
        public AuthorizeWcfServiceClient(ChannelFactory<IAuthorize> channelFactory) : base(channelFactory)
        {
            
        }

        
    }
}