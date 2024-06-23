using BankApp.Classes;
using BankApp.Database;
using BankApp.Models;
using ConsoleTableExt;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Services.TransactionHistoryServices
{
    internal class TransactionHistoryService : ITransactionHistoryService
    {
        public void DisplayTransactionHistory(Guid userId)
        {
            //BankApp_DbContext db = new BankApp_DbContext();

            using (BankApp_DbContext db = new BankApp_DbContext())
            {

                List<TransactionHistory> transactions = db.GetUserTransactions(userId);

                if (transactions.Count == 0)
                {
                    Console.WriteLine("No transactions found.");
                    return;
                }

                ConsoleTableBuilder
                    .From(transactions.Select(t => new
                    {
                        t.Id,
                        t.TransactionType,
                        t.Amount,
                        t.Timestamp
                    }).ToList())
                    .WithFormat(ConsoleTableBuilderFormat.Alternative)
                    .ExportAndWriteLine();
            }

        }

        public void ViewTransactionHistory(User sessionUser)
        {
            using (BankApp_DbContext db = new BankApp_DbContext())
            {

                DisplayTransactionHistory(sessionUser.Id);

            }
        }


    }
}

