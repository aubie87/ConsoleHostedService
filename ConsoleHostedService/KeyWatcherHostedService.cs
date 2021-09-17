using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleHostedService
{
    internal sealed class KeyWatcherHostedService : IHostedService
    {
        private readonly ILogger<KeyWatcherHostedService> logger;
        private readonly IHostApplicationLifetime appLifetime;

        public KeyWatcherHostedService(ILogger<KeyWatcherHostedService> logger, IHostApplicationLifetime appLifetime)
        {
            this.logger = logger;
            this.appLifetime = appLifetime;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation("StartAsync started!");
            await Task.Delay(1000, cancellationToken);
            
            logger.LogInformation("StartAsync - registering events");
            appLifetime.ApplicationStarted.Register(OnStarted);
            appLifetime.ApplicationStopping.Register(OnStopping);
            appLifetime.ApplicationStopped.Register(OnStopped);
        }

        private void OnStopped()
        {
            logger.LogInformation("OnStopped");
        }

        private void OnStopping()
        {
            logger.LogInformation("OnStopping");
        }

        private void OnStarted()
        {
            logger.LogInformation("OnStarted has started!");
            Task.Run(async () =>
            {
                logger.LogInformation("Inside Task.Run - press 'q' to quit.");
                await Task.Delay(1000);

                logger.LogInformation("Inside Task.Run - reading keyboard");
                while(Console.ReadKey(true).KeyChar != 'q')
                {
                    logger.LogInformation("key pressed");
                }
                appLifetime.StopApplication();
            });
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation("StopAsync stopping...");
            await Task.Delay(1000, cancellationToken);
            Environment.ExitCode = 1;
        }
    }
}
