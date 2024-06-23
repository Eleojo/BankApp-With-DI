using BankApp.Classes;
using BankApp.Database;
using BankApp.Models;
using BankApp.Services.AccountServices;
using BankApp.Services.TransactionHistoryServices;
using BankApp.Utilities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace BankApp.Services.TransactionServices
{
    public class TransactionService : ITransactionService
    {
        private readonly IAccountService _accountService;

        public TransactionService(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public async Task WithdrawMoney(User sessionUser)
        {
            //BankApp_DbContext db = new BankApp_DbContext();
            //List<User> users = db.GetAllEntities<User>();

            using (BankAppDbContext db = new BankAppDbContext())
            {

                var accounts = await db.GetAllEntities<Account>();

                // Find the user with the specified account number
                Account foundAccount = accounts.FirstOrDefault(account => account.userId.Equals(sessionUser.Id));

                // Prompt for the amount to withdraw
                decimal withdrawAmount = Validations.GetValidInput("Enter amount to withdraw:", Validations.IsValidAmount, "Use Numbers.Please try again.");

                // Check if the account was found
                if (foundAccount != null)
                {
                    // Check if there are sufficient funds in the account
                    if (withdrawAmount > foundAccount.accountBalance)
                    {
                        Console.WriteLine("Insufficient Funds");
                    }
                    else
                    {
                        // Perform the withdrawal
                        foundAccount.accountBalance -= withdrawAmount;
                        db.UpdateEntities(accounts); // Update database
                        TransactionHistory RecordedTransaction = new TransactionHistory(foundAccount.userId, "Credit", withdrawAmount, sessionUser.FirstName, "Self");
                        db.AddEntity(RecordedTransaction);
                        Console.WriteLine($"Success !! You have withdrawn {withdrawAmount} and your new balance is {foundAccount.accountBalance} ");
                        _accountService.DisplayAccountInfo(sessionUser);
                        Console.WriteLine("Press Enter key to return to main menu");
                    }
                }
                else
                {
                    Console.WriteLine("Sorry, you do not have an account with us.");
                }
            }
        }

        public async Task DepositMoney(User sessionUser)
        {
            //BankApp_DbContext db = new BankApp_DbContext();
            //List<User> users = db.GetAllEntities<User>();
            using (BankAppDbContext db = new BankAppDbContext())
            {

                var accounts = await db.GetAllEntities<Account>();

                decimal depositAmount = Validations.GetValidInput("Enter amount to deposit:", Validations.IsValidAmount, "Use Numbers.Please try again.");


                // Find the account associated with the user
                Account foundAcount = accounts.FirstOrDefault(account => account.userId.Equals(sessionUser.Id));
                // Check if the account was found

                if (foundAcount != null)
                {
                    // Perform the deposit
                    //foundAccount.accountBa += depositAmount;
                    foundAcount.accountBalance += depositAmount;

                    db.UpdateEntities(accounts); // Update database
                    TransactionHistory RecordedTransaction = new TransactionHistory(foundAcount.Id, "Credit", depositAmount, sessionUser.FirstName,"Self");
                    db.AddEntity(RecordedTransaction);
                    db.SaveChanges();

                    Console.WriteLine($"Success!! You have deposited {depositAmount} and your new balance is {foundAcount.accountBalance}.");
                    _accountService.DisplayAccountInfo(sessionUser);
                    Console.WriteLine("Press Enter key to return to main menu");
                }
                else
                {
                    Console.WriteLine("Sorry, you do not have an account with us.");
                }

            }


        }

        public async Task Transfer(User sessionUser)
        {
            BankAppDbContext db = new BankAppDbContext();
            List<User> users = await db.GetAllEntities<User>();
            List<Account> accounts = await db.GetAllEntities<Account>();


            //string amount = Console.ReadLine();
            decimal transferAmount = Validations.GetValidInput("Enter amount to Transfer: ", Validations.IsValidAmount, "Use Numbers. Please try again.");

            Console.WriteLine("Enter receiver accNumber:");
            string receiverAccountNumber = Console.ReadLine();

            // Find sender and receiver user IDs using LINQ
            Account senderAccount = accounts.SingleOrDefault(account => account.userId.Equals(sessionUser.Id));
            Account receiverAccount = accounts.SingleOrDefault(account => account.accountNumber.Equals(receiverAccountNumber));
            User receiver = null;

            // Check if both users were found
            if (senderAccount == null || receiverAccount == null)
            {

                Console.WriteLine($"One or both users not found. Please make sure both sender and receiver are registered.");
                return;
            }
            else
            {
                receiver = users.SingleOrDefault(user => user.Id.Equals(receiverAccount.userId));
            }
            // Check if senderAccount balance less than transfer amount
            if (senderAccount.accountBalance < transferAmount)
            {
                Console.WriteLine("Insufficient funds in the sender's account.");
                return;
            }

            // Perform the transfer
            senderAccount.accountBalance -= transferAmount;
            receiverAccount.accountBalance += transferAmount;

            // Update changes to database
            db.UpdateEntities(accounts);
            Console.WriteLine("Enter description: ");
            string description = Console.ReadLine();

            // Create Trasaction History
            TransactionHistory senderTransaction = new TransactionHistory(senderAccount.userId, "Debit", transferAmount, receiver.FirstName, description);
            TransactionHistory receiverTransaction = new TransactionHistory(senderAccount.userId, "Credit", transferAmount, sessionUser.FirstName, description);

            db.AddEntity(senderTransaction);
            db.AddEntity(receiverTransaction);

            _accountService.DisplayAccountInfo(sessionUser);

            Console.WriteLine($"Transfer successful! {transferAmount} has been transferred to {receiver.FirstName}.");
            Console.WriteLine($"Sender's new balance: {senderAccount.accountBalance}");
            Console.WriteLine($"Receiver's new balance: {receiverAccount.accountBalance}");
        }

    }
}
