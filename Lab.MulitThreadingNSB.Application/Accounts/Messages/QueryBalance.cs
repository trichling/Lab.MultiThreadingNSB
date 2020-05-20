using System;

namespace Lab.MutliThreadingNSB.Application.Accounts.Messages
{
    public class QueryBalanceRequest
    {
        public QueryBalanceRequest(Guid accountId)
        {
            AccountId = accountId;
        }
        public Guid AccountId { get; set; }

    }

    public class QueryBalanceReply
    {
        public QueryBalanceReply(Guid accountId, decimal balance)
        {
            this.Balance = balance;
            AccountId = accountId;
        }

        public Guid AccountId { get; set; }
        public decimal Balance { get; set; }

    }

}