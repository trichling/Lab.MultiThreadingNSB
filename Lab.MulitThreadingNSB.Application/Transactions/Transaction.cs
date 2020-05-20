using System;
using System.Threading.Tasks;
using Lab.MutliThreadingNSB.Application.Accounts.Messages.Commands;
using Lab.MutliThreadingNSB.Application.Accounts.Messages.Events;
using Lab.MutliThreadingNSB.Application.Transactions.Messages.Commands;
using NServiceBus;

namespace Lab.MutliThreadingNSB.Application.Transactions
{
    public class Transaction : Saga<TransactionData>
        , IAmStartedByMessages<Transfer>
        , IHandleMessages<AmountWithdrawn>
        , IHandleMessages<AmountDeposited>
    {
        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<TransactionData> mapper)
        {
            mapper.ConfigureMapping<Transfer>(m => m.TransactionId).ToSaga(s => s.TransactionId);
            mapper.ConfigureMapping<AmountWithdrawn>(m => m.TransactionId).ToSaga(s => s.TransactionId);
            mapper.ConfigureMapping<AmountDeposited>(m => m.TransactionId).ToSaga(s => s.TransactionId);
        }

        public async Task Handle(Transfer message, IMessageHandlerContext context)
        {
            Data.TransactionId = message.TransactionId;
            Data.SourceAccountId = message.SourceAccountNumber;
            Data.DestinationAccountId = message.TargetAccountNumber;

            await context.SendLocal(new Withdraw(message.TransactionId, message.SourceAccountNumber, message.Amount));
        }

        public async Task Handle(AmountWithdrawn message, IMessageHandlerContext context)
        {
            await context.SendLocal(new Deposit(message.TransactionId, Data.DestinationAccountId, message.Amount));
        }

        public async Task Handle(AmountDeposited message, IMessageHandlerContext context)
        {
            Data.TransferCompleted = true;

            await Task.CompletedTask;
        }
    }

    public class TransactionData : ContainSagaData
    {

        public Guid TransactionId { get; set; }
        public Guid SourceAccountId { get; set; }
        public Guid DestinationAccountId { get; set; }

        public bool TransferCompleted { get; set; }
    }
}