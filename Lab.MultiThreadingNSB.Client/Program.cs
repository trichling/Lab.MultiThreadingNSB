using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lab.MutliThreadingNSB.Application.Accounts.Messages;
using Lab.MutliThreadingNSB.Application.Accounts.Messages.Commands;
using Lab.MutliThreadingNSB.Application.Transactions.Messages.Commands;
using Lab.MutliThreadingNSB.Host;
using NServiceBus;

namespace Lab.MultiThreadingNSB.Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var host = new EndpointHost();
            var endpoint = await Endpoint.Start(host.ConfigureSendOnlyApiEndpoint().Configuration);

            Console.WriteLine("Send only endpoint started.");
            Console.WriteLine("Press any key to start.");
            Console.ReadLine();

            //await SimpleTransfer(endpoint);
            //await TheFundraiser(endpoint);
            await TheMillionairesGame(endpoint);

            Console.ReadLine();
        }

        private static async Task SimpleTransfer(IEndpointInstance endpoint)
        {
            var accountOfBob = Guid.NewGuid();
            var accountOfSam = Guid.NewGuid();

            await endpoint.Send(new Open(accountOfBob, 1000000.0M));
            await endpoint.Send(new Open(accountOfSam, 1000000.0M));

            await Task.Delay(1000);

            await endpoint.Send(new Transfer(Guid.NewGuid(), accountOfBob, accountOfSam, 1.0M));

            await Task.Delay(1000);

            //await endpoint.Send(new QueryBalanceRequest(accountOfSam));
        }


        private static async Task TheFundraiser(IEndpointInstance endpoint)
        {
            var accountOfTesla = Guid.NewGuid();
            await endpoint.Send(new Open(accountOfTesla, 0.0M));

            await Task.Delay(1000);

            var tasks = new List<Task>();
            for (int i = 0; i < 100; i++)
            {
                tasks.Add(Task.Run(async () =>
                {
                    await endpoint.Send(new Deposit(Guid.NewGuid(), accountOfTesla, 1.0M));
                }));
            }

            Task.WaitAll(tasks.ToArray());

            // string input = string.Empty;
            // do {
            //     await endpoint.Send(new QueryBalanceRequest(accountOfTesla));
            //     input = Console.ReadLine();
            // } while ( input != "q" );
        }

        private static async Task TheMillionairesGame(IEndpointInstance endpoint)
        {
            var transactionCount = 1000;

            var accountOfBob = Guid.NewGuid();
            var accountOfSam = Guid.NewGuid();

            await endpoint.Send(new Open(accountOfBob, 1000000.0M));
            await endpoint.Send(new Open(accountOfSam, 1000000.0M));

            Console.WriteLine("Open accounts");
            Console.ReadLine();

            var bobToSam = Task.Run(async () =>
            {
                for (int i = 0; i < transactionCount; i++)
                {
                    await endpoint.Send(new Transfer(Guid.NewGuid(), accountOfBob, accountOfSam, 1.0M));
                }
            });

            var samToBob = Task.Run(async () =>
            {
                for (int i = 0; i < transactionCount; i++)
                {
                    await endpoint.Send(new Transfer(Guid.NewGuid(), accountOfSam, accountOfBob, 1.0M));
                }
            });

            Task.WaitAll(bobToSam, samToBob);

            // string input = string.Empty;
            // do {
            //     await endpoint.Send(new QueryBalanceRequest(accountOfSam));
            //     await endpoint.Send(new QueryBalanceRequest(accountOfBob));
            //     input = Console.ReadLine();
            // } while ( input != "q" );
            

            await Task.Delay(1000);
        }
    }

    
}
