using System;

namespace Lab.MutliThreadingNSB.Application.Accounts.Messages.Commands
{
    public class Open
    {
        
        public Open(Guid accountId, decimal initialBalance)
        {
            AccountId = accountId;
            InitialBalance = initialBalance;
        }

        public Guid AccountId { get; set; }
        public decimal InitialBalance { get; set; }

    }

}