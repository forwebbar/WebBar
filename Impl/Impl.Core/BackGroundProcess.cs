using System;
using System.Threading;
using Common.Logging;

namespace Impl.Core
{
    public class BackGroundProcess : IStart
    {
        private Thread _worker;
        private bool _terminated;
        private readonly ILog _logger = LogManager.GetLogger<BackGroundProcess>();
        private readonly IExceutable _exceutable;

        public BackGroundProcess(IExceutable exceutable)
        {
            _exceutable = exceutable;
        }

        public void Start()
        {
            _terminated = false;
            _worker = new Thread(DoWork){IsBackground = true};
            _worker.Start();            
        }

        private void DoWork()
        {
            _logger.Info("BackGroundProcess started");
            while (!_terminated)
            {
                try
                {
                    _exceutable.RunSingleCicle();
                }
                catch (Exception e)
                {
                    _logger.Error("ReaderDispatcher Error", e);
                }
                finally
                {
                    Thread.Sleep(500);
                }
            }
            _logger.Info("BackGroundProcess terminated");
        }

        public void Stop()
        {
            _terminated = true;
        }
    }
}