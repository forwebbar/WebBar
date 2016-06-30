using System.Collections.Generic;
using System.Linq;
using Impl.DAL;

namespace WebBar.BeerServer.DataCollector
{
    internal class DeviceCache : IDeviceCache
    {
        private readonly Dictionary<string, DbDevice> _devices = new Dictionary<string, DbDevice>(); 

        public void Refresh()
        {
            using (var context = new BeerControlEntities())
            {
                var dbDevices = context.Device.Select(d=> new { d.id, d.Uid, d.idMarket} ).ToList();
                foreach (var dbDevice in dbDevices)
                {
                    if (_devices.ContainsKey(dbDevice.Uid))
                    {
                        _devices[dbDevice.Uid].Id = dbDevice.id;
                        _devices[dbDevice.Uid].IdMarket = dbDevice.idMarket;
                    }
                    else
                    {
                        _devices.Add(dbDevice.Uid, new DbDevice
                        {
                            Id = dbDevice.id,
                            IdMarket = dbDevice.idMarket
                        });
                    }
                }

            }
        }

        public DbDevice GetDevice(string uid)
        {
            if (_devices.ContainsKey(uid))
                return DbDevice.Copy(_devices[uid]);

            return null;
        }
    }
}
