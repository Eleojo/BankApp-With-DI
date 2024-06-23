using BankApp.Classes;
using BankApp.Database;
using ConsoleTableExt;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Models
{
    public class TransactionHistory
    {
        public Guid Id { get; set; }
        public Guid AccountId { get; set; }
        //public Guid UserId { get; set; } // Add this propertz
        public string TransactionType { get; set; } // e.g., "Deposit", "Withdrawal", "Transfer"
        public decimal Amount { get; set; }
        public string sender { get; set; }
        public string description { get; set; }
        public DateTime Timestamp { get; set; }

        

        public TransactionHistory(Guid accountId, string transactionType, decimal amount, string sender, string description)
        {
            Id = new Guid();
            AccountId = accountId;
            TransactionType = transactionType;
            Amount = amount;
            this.sender = sender;
            this.description = description;
            Timestamp = DateTime.Now;
        }
    }

}
