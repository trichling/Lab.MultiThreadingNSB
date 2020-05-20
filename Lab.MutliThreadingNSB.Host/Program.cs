using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using NServiceBus;

namespace Lab.MutliThreadingNSB.Host
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var host = new EndpointHost();

            await host.Start();
            Console.WriteLine($"Endpoint {EndpointHost.EndpointName} is running. Press any key to shutdown.");

            //await SimpleTransfer(host.Endpoint);
            //await TheFundraiser(host.Endpoint);
            //await TheMillionairesGame(host.Endpoint);

            Console.ReadLine();            

            await host.Stop();
        }

      

    }

   
}


