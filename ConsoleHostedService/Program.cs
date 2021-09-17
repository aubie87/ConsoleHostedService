using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;

namespace ConsoleHostedService
{
    internal sealed class Program
    {
        private static async Task Main(string[] args)
        {
            await Host.CreateDefaultBuilder(args)
                .ConfigureServices((context,services)=>
                {
                    services.AddHostedService<KeyWatcherHostedService>();
                })
                .RunConsoleAsync(RunThis);
        }

        private static void RunThis(ConsoleLifetimeOptions obj)
        {
            Console.WriteLine("Inside RunThis");
        }
    }
}
