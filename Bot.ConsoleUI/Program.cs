using System;
using System.Threading.Tasks;
using Bot.ConsoleUI.InversionOfControl;
using Microsoft.Extensions.Hosting;

namespace Bot.ConsoleUI
{

    class Program
    {
        static Task Main(string[] args) =>
            Container.CreateHostBuilder(args).Build().RunAsync();
    }
}
