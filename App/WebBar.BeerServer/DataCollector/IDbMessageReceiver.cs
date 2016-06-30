using Contracts;

namespace WebBar.BeerServer.DataCollector
{
    internal interface IDbMessageReceiver
    {
        void Add(MessageDto dto);
    }
}