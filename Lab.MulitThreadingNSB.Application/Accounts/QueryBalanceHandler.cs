using System;
using System.Threading.Tasks;
using Lab.MutliThreadingNSB.Application.Accounts.Messages;
using NServiceBus;

namespace Lab.MulitThreadingNSB.Application.Accounts
{
    public class QueryBalanceHandler : IHandleMessages<QueryBalanceReply>
    {
        public Task Handle(QueryBalanceReply message, IMessageHandlerContext context)
        {
            Console.WriteLine($"Balance of {message.AccountId} is {message.Balance}");

            return Task.CompletedTask;
        }
    }
}