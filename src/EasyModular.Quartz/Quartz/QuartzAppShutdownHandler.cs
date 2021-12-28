using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EasyModular.Quartz
{
    public class QuartzAppShutdownHandler : IAppShutdownHandler
    {
        private readonly IQuartzServer _server;
        private readonly ILogger _logger;

        public QuartzAppShutdownHandler(IQuartzServer server, ILogger<QuartzAppShutdownHandler> logger)
        {
            _server = server;
            _logger = logger;
        }

        public async Task Handle()
        {
            if (_server != null)
            {
                await _server.Stop();

                _logger.LogDebug("quartz server stop");
            }
        }
    }
}
