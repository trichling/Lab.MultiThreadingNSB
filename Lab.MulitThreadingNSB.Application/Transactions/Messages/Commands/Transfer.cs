using System;

namespace Lab.MutliThreadingNSB.Application.Transactions.Messages.Commands
{
    public class Transfer
    {
        public Transfer(Guid transactionId, Guid sourceAccountNumber, Guid targetAccountNumber, decimal amount)
        {
            TransactionId = transactionId;
            SourceAccountNumber = sourceAccountNumber;
            TargetAccountNumber = targetAccountNumber;
            Amount = amount;
        }

        public Guid TransactionId { get; set; }
        public Guid SourceAccountNumber { get; set; }
        public Guid TargetAccountNumber { get; set; }
        public decimal Amount { get; set; }
    }
}