using BankApp.Classes;
using BankApp.Database;
using ConsoleTableExt;
using ConsoleTables;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Services.AccountServices
{
    internal class AccountService : IAccountService
    {

        public void UpdateMyAccountProfile()
        {

        }



        public void OpenAccount(Guid userId)
        {
            BankApp_DbContext db = new BankApp_DbContext();
            List<User> users = db.GetAllEntities<User>();
            List<Account> accounts = db.GetAllEntities<Account>();

            Console.WriteLine("Choose Account Type you want to open: ");
            Console.WriteLine("Press 1 for Current:");
            Console.WriteLine("Press 2 for Savings:");

            string accountTypeString = Console.ReadLine();
            string accountType = string.Empty;

            if (accountTypeString == "1")
            {
                accountType = "current";
            }
            else if (accountTypeString == "2")
            {
                accountType = "savings";
            }
            else
            {
                Console.WriteLine("Invalid input");
                return; // Exit the method if input is invalid
            }

            string accountNumber = GenerateRandomAccountNumber();  // Generate the 10-digit account number
            decimal initialAccountBalance = 0;

            // Check if the user already has the same type of account
            //bool accountExists = accounts.Any(a => a.userId.Equals(userId) && a.accountType.Equals(accountType));
            User user = users.FirstOrDefault(u => u.Id.Equals(userId));

            var accountExists = (from u in users
                                 join a in accounts on u.Id equals a.userId
                                 where u.Email.Equals(user.Email, StringComparison.OrdinalIgnoreCase)
                                       && a.accountType.Equals(accountType, StringComparison.OrdinalIgnoreCase)
                                 select a).Any();

            if (accountExists)
            {
                Account existingAccount = accounts.SingleOrDefault(a => a.userId == userId && a.accountType.Equals(accountType));



                if (existingAccount != null)
                {
                    string oppositeAccountType = accountType == "current" ? "savings" : "current";
                    Console.WriteLine($"You already have a {existingAccount.accountType} account with us. To open another account, it must be a {oppositeAccountType} account.");
                    Console.WriteLine("\nPress any key to continue ");
                    Console.ReadKey();
                }
                //return;
            }
            else
            {
                // If no account exists, create a new one
                Account newAccount = new Account(userId, initialAccountBalance, accountNumber, accountType);
                db.AddEntity(newAccount);
                Console.WriteLine($"\nAccount created successfully for user {user.FirstName} with account number {accountNumber}.");
                Console.WriteLine("\nPress any key to continue after copying your account number...");
                Console.ReadKey();
            }
        }

        // Ensure that GenerateRandomAccountNumber() method generates unique account numbers
        private static string GenerateRandomAccountNumber()
        {
            Random random = new Random();
            string accountNumber;
            do
            {
                accountNumber = random.Next(1000000000, 1999999999).ToString(); // Ensures a 10-digit number
            } while (!IsUniqueAccountNumber(accountNumber));

            return accountNumber;
        }

        // Method to check if account number is unique
        private static bool IsUniqueAccountNumber(string accountNumber)
        {
            using (var db = new BankApp_DbContext())
            {
                List<Account> accounts = db.GetAllEntities<Account>();
                return !accounts.Any(a => a.accountNumber == accountNumber);
            }
        }

        public static void DisplayAccountInfo(User sessionUser)
        {
            BankApp_DbContext db = new BankApp_DbContext();
            List<Account> accounts = db.GetAllEntities<Account>();
            List<User> users = db.GetAllEntities<User>();

            // Use LINQ to filter users and join with accounts
            var accountDisplays = users
                .Where(user => user.FirstName.Equals(sessionUser.FirstName))
                .Join(accounts,
                      user => user.Id,
                      account => account.userId,
                      (user, account) => new AccountDisplay
                      {
                          FullName = $"{user.FirstName} {user.LastName}",
                          AccountNumber = account.accountNumber,
                          AccountType = account.accountType,
                          AccountBalance = account.accountBalance,
                          UserId = account.userId
                      })
                .ToList();

            if (accountDisplays.Any())
            {
                ConsoleTableBuilder
                    .From(accountDisplays)
                    .WithFormat(ConsoleTableBuilderFormat.Alternative)
                    .ExportAndWriteLine();
                // showAllDb(accountDisplays);
            }
            else
            {
                Console.WriteLine("User not found in database");
            }
        }
        public void showAllDb<T>(List<T> obj) where T : class
        {
            ConsoleTableBuilder
                .From(obj)
                .WithFormat(ConsoleTableBuilderFormat.Alternative)
                .ExportAndWriteLine();
        }

    }
}

