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
        public async Task DisplayTransactionHistory(User sessionUser)
        {
            //BankApp_DbContext db = new BankApp_DbContext();

            using (BankAppDbContext db = new BankAppDbContext())
            {
                List<Account> accounts = await db.GetAllEntities<Account>();
                Account foundAcount = accounts.FirstOrDefault(account => account.userId.Equals(sessionUser.Id));
                var transactions =  db.GetUserTransactions(foundAcount.Id);

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

       


    }
}

