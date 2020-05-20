using System;

namespace Lab.MutliThreadingNSB.Application.Accounts.Messages.Events
{
    public class AccountOpened 
    {
        
        public AccountOpened(Guid accountId, decimal initialBalance)
        {
            AccountId = accountId;
            InitialBalance = initialBalance;
        }

        public Guid AccountId { get; set; }
        public decimal InitialBalance { get; set; }

    }
}