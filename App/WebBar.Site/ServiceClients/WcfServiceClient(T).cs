using System;
using System.ServiceModel;

namespace WebBar.Site.ServiceClients
{
    public class WcfServiceClient<T> where T : class
    {
        #region Constructors
        
        public WcfServiceClient(ChannelFactory<T> channelFactory)
        {
            _channelFactory = channelFactory;
        }

        #endregion

        #region Fields

        private readonly ChannelFactory<T> _channelFactory;

        #endregion

        #region Methods

        public IClientChannel GetClient()
        {
            var client = (IClientChannel)_channelFactory.CreateChannel();
            return client;
        }

        public TResult Execute<TResult>(Func<T, TResult> func)
        {
            var client = GetClient();

            try
            {
                var result = func((T)client);
                client.Close();
                return result;
            }
            catch (Exception)
            {
                if (client.State == CommunicationState.Faulted)
                {
                    client.Abort();
                    _channelFactory.Abort();
                }
                else if (client.State != CommunicationState.Closed)
                {
                    client.Close();
                    _channelFactory.Close();
                }
                throw;
            }
        }

        public void Execute(Action<T> action)
        {
            Execute(f =>
            {
                action(f);
                return (object)null;
            });
        }

        #endregion
    }
}