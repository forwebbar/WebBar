using System.Collections.Generic;
using System.Linq;
using Contracts;
using Impl.DAL;

namespace WebBar.BeerServer.DataCollector
{
    internal class DbMessageReceiver : IDbMessageReceiver
    {
        private readonly IDeviceCache _cache = new DeviceCache();

        internal DbMessageReceiver()
        {
            _cache.Refresh();
        }

        public void Add(MessageDto dto)
        {
            var device = _cache.GetDevice(dto.SerialNumber);
            if(device == null)
                return;

            var fills = GetFills(device, dto);
            if(!fills.Any())
                return;

            device.AddFills(fills);
        }

        private List<Fill> GetFills(DbDevice dbDevice, MessageDto dto)
        {
            var list = new List<Fill>();
            const string prefix = "PORT";
            const string opCodePrefix = "NUMEMAIL";

            int opCode=0;
            foreach (var tag in dto.Tags.ToArray())
            {

                if (tag.Key == opCodePrefix)
                    int.TryParse(tag.Value, out opCode);


                if (!tag.Key.Contains(prefix))
                    continue;

                var tapCodeStr = tag.Key.Substring(prefix.Length);
                int tapCode;
                if(!int.TryParse(tapCodeStr, out tapCode))
                    continue;

                int volume;
                if(!int.TryParse(tag.Value, out volume))
                    continue;
                
                list.Add(new Fill
                {
                    idDevice = dbDevice.Id,
                    TapCode = tapCode,
                    Ts = dto.TimeStamp,
                    Volume = volume,
                    Total = GetTotal(tapCode, dto),
                    OperationCode = opCode
                });
            }

            return list;
        }

        private long GetTotal(int tapCode, MessageDto dto)
        {
            const string totalPrefix = "FLMETR";
            var key = totalPrefix + tapCode;
            long total;
            return dto.Tags.ContainsKey(key) && long.TryParse(dto.Tags[key], out total) ? total : 0;
        }
    }
}