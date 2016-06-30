using Contracts;

namespace WebBar.BeerServer.DataCollector
{
    internal interface IMessageAnalizer
    {
        MessageDto Process(MessageDto item);
    }
}