using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using Common.Logging;
using Contracts;
using Impl.Core;

namespace WebBar.BeerServer.DataCollector
{
    public class FileCollector : IExceutable
    {
        private readonly string _folder;
        private readonly int _cycleLimit;

        private IMessageAnalizer _analizer;
        private IDbMessageReceiver _receiver;
        private readonly ILog _logger = LogManager.GetLogger<FileCollector>();

        public FileCollector(string folder, int cycleLimit)
        {
            _folder = folder;
            _cycleLimit = cycleLimit;           
            _analizer = new MessageAnalizer();
            _receiver = new DbMessageReceiver();
        }

        public void RunSingleCicle()
        {
            var minDate = new DateTime(2015, 1, 1);
            var files = GetFiles(_cycleLimit);
            foreach (var file in files)
                _logger.InfoFormat("File {0} ", file.Path);

            var items = ParseFiles(files);
            foreach (var item in items)
            {
                _logger.InfoFormat("Device {0} Ts {1}", item.SerialNumber, item.TimeStamp);
                var dto = _analizer.Process(item);
                if (item.TimeStamp < minDate)
                {
                    _logger.Error(string.Format("Device {0} Timestamp is less then minimum. Ts = {1}", item.SerialNumber, item.TimeStamp));
                    continue;
                }

                if (dto != null)
                    _receiver.Add(dto);
                else
                    _logger.InfoFormat("Device {0} Ts {1} dto == null", item.SerialNumber, item.TimeStamp);
            }

            DeleteFiles(files);
        }

        private List<MessageDto> ParseFiles(List<FileItem> files)
        {
            var list = new List<MessageDto>();
            var ns = new NetDataContractSerializer();
            foreach (var file in files)
            {
                try
                {
                    using (var m = new MemoryStream(file.Data))
                    {
                        var messageDto = ns.ReadObject(m) as MessageDto;
                        if (messageDto == null || string.IsNullOrEmpty(messageDto.DeviceCode) ||
                            string.IsNullOrEmpty(messageDto.SerialNumber) || !messageDto.Tags.Any())
                            continue;

                        list.Add(messageDto);
                    }
                }
                catch (Exception e)
                {
                    _logger.ErrorFormat("ParseFiles Error {0}", e, file.Path);
                }
            }

            return list;
        }

        private List<FileItem> GetFiles(int cycleLimit)
        {
            var list = new List<FileItem>(cycleLimit);
            int cntr = 0;
            var dirs = Directory.GetDirectories(_folder);
            foreach (var dir in dirs)
            {
                var files = Directory.GetFiles(dir);
                foreach (var path in files)
                {
                    if (cntr >= cycleLimit)
                        return list;

                    list.Add(new FileItem
                    {
                        Path = path,
                        Data = ReadFileData(path)
                    });

                    cntr++;
                }
            }

            return list;
        }

        private byte[] ReadFileData(string path)
        {
            using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                var length = stream.Length;
                var data = new byte[length];
                stream.Read(data, 0, (int)length);
                return data;
            }
        }

        private static void DeleteFiles(List<FileItem> files)
        {
            foreach (var file in files)
            {
                if (File.Exists(file.Path))
                    File.Delete(file.Path);
            }
        }

        private class FileItem
        {
            public byte[] Data;
            public string Path;
        }
    }
}