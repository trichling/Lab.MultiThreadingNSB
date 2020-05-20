using System;
using System.Threading.Tasks;
using NServiceBus;
using Lab.MutliThreadingNSB.Application.Accounts.Messages.Commands;
using Lab.MutliThreadingNSB.Application.Accounts.Messages;
using Lab.MutliThreadingNSB.Application.Accounts.Messages.Events;

namespace Lab.MutliThreadingNSB.Application.Accounts
{
    public class Account : Saga<AccountData>,
        IAmStartedByMessages<Open>,
        IHandleMessages<Deposit>,
        IHandleMessages<Withdraw>,
        IHandleMessages<QueryBalanceRequest>
    {
        public Account()
        {
        }

        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<AccountData> mapper)
        {
            mapper.ConfigureMapping<Open>(m => m.AccountId).ToSaga(s => s.AccountId);
            mapper.ConfigureMapping<Deposit>(m => m.AccountId).ToSaga(s => s.AccountId);
            mapper.ConfigureMapping<Withdraw>(m => m.AccountId).ToSaga(s => s.AccountId);

            mapper.ConfigureMapping<QueryBalanceRequest>(m => m.AccountId).ToSaga(s => s.AccountId);
        }

        public async Task Handle(Open message, IMessageHandlerContext context)
        {
            Data.AccountId = message.AccountId;
            Data.Balance = message.InitialBalance;

            await context.Publish(new AccountOpened(Data.AccountId, Data.Balance));
        }

        public async Task Handle(QueryBalanceRequest message, IMessageHandlerContext context)
        {
            await context.Reply(new QueryBalanceReply(Data.AccountId, Data.Balance));
        }

        public async Task Handle(Deposit message, IMessageHandlerContext context)
        {
            Data.Balance += message.Amount;

            await context.Publish(new AmountDeposited(message.TransactionId, Data.AccountId, message.Amount));

        }

        public async Task Handle(Withdraw message, IMessageHandlerContext context)
        {
            Data.Balance -= message.Amount;

            await context.Publish(new AmountWithdrawn(message.TransactionId, Data.AccountId, message.Amount));
        }

        


    }

    public class AccountData : ContainSagaData
    {
        public Guid AccountId { get; set; }

        public decimal Balance { get; set; }
    }

}