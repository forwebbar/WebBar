namespace WebBar.BeerServer.DataCollector
{
    internal interface IDeviceCache
    {
        void Refresh();
        DbDevice GetDevice(string uid);
    }
}