﻿using BankApp.Classes;
using BankApp.Database;
using BankApp.Services;
using BankApp.Services.AccountServices;
using BankApp.Services.AuthServices;
using BankApp.Services.DashBoardServices;
using BankApp.Services.TransactionHistoryServices;
using BankApp.Services.TransactionServices;
using BankApp.Services.UserServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp
{
    public  class Run
    {
        private readonly IUserService _userService;
        private readonly IAccountService _accountService;
        private readonly IAuthService _authService;
        private readonly IDashBoardService _dashBoardService;
        private readonly ITransactionService _transactionsService;
        private readonly ITransactionHistoryService _transactionHistoryService;

       

        public Run()
        {

        }

        public Run(IUserService userService, IAccountService accountService, IAuthService authService, IDashBoardService dashBoardService, ITransactionService transactionsService, ITransactionHistoryService transactionHistoryService)
        {
            _userService = userService;
            _accountService = accountService;
            _authService = authService;
            _dashBoardService = dashBoardService;
            _transactionsService = transactionsService;
            _transactionHistoryService = transactionHistoryService;
        }

        public async Task StartApp()
        {

            bool loggedIn = false;
            //User sessionUser = null;

            while (!loggedIn)
            {
                Console.Clear();
                LandingPageMenu();
                string response = Console.ReadLine();
                Guid userId;

                if (response == "1")
                {

                    //UserService userMethod = new UserService();
                    //userId = userMethod.RegisterUser();

                   _userService.RegisterUser();

                    Console.WriteLine("would you like to open an account now?  Y/N");
                    string choice = Console.ReadLine().ToLower();

                    if (choice == "y")
                    {
                        //AccountService.OpenAccount(userId);
                        _accountService.OpenAccount(UserSession.RegisteredUser.Id);
                    }
                    else
                    {
                        Console.WriteLine("Thank you for registering consider creating an account");

                    }

                }
                else if (response == "2")
                {
                    Console.Write("Enter Username(Your Email): ");
                    string email = Console.ReadLine();
                    Console.Write("Enter Password: ");
                    //var authServices = new AuthService();

                    string password = _authService.ReadPassword();

                    if (_authService.AuthenticateUser(email, password))
                    {
                        loggedIn = true;
                        Console.WriteLine("\nLogin Successful!");
                    }
                    else
                    {
                        Console.WriteLine("\nInvalid username or password. Please try again.");
                        Console.ReadKey();
                    }
                }
                else if (response == "3")
                {
                    Console.WriteLine("Exiting...");
                    Thread.Sleep(2000);
                    break;
                }
                else
                {
                    Console.WriteLine("Incorrect Response");
                }


                while (loggedIn)
                {
                    Console.Clear();
                    //UserSession userSession = new UserSession();
                    _dashBoardService.ApplicationDashBoard(UserSession.LoggedInUser);

                    string userInput = Console.ReadLine();

                    if (string.IsNullOrEmpty(userInput))
                    {
                        Console.WriteLine("Please input a valid option.");
                    }

                    else if (userInput == "1")
                    {
                        _transactionsService.WithdrawMoney(UserSession.LoggedInUser);
                        Console.WriteLine("\nPress any key to continue ");
                        Console.ReadKey();
                    }
                    else if (userInput == "2")
                    {
                        _transactionsService.DepositMoney(UserSession.LoggedInUser);
                        Console.WriteLine("\nPress any key to continue ");
                        Console.ReadKey();
                    }
                    else if (userInput == "3")
                    {
                        _accountService.DisplayAccountInfo(UserSession.LoggedInUser);
                        Console.WriteLine("\nPress any key to continue ");
                        Console.ReadKey();
                    }
                    else if (userInput == "4")
                    {

                        _transactionsService.Transfer(UserSession.LoggedInUser);
                        Console.WriteLine("\nPress any key to continue ");
                        Console.ReadKey();
                    }
                    else if (userInput == "5")
                    {
                        using (var db = new BankAppDbContext())
                        {
                           var users = await db.GetAllEntities<User>();
                            var accounts = await db.GetAllEntities<Account>();

                            Console.WriteLine("What Database would you like to see");
                            string dbType = Console.ReadLine().ToLower();
                           // var show = new AccountService();

                            if (dbType == "users")
                            {
                                _accountService.ShowAllDb(users);
                            }
                            else if (dbType == "accounts")
                            {
                                _accountService.ShowAllDb(accounts);
                            }
                            Console.WriteLine("\nPress any key to continue...");
                            Console.ReadKey();
                        }
                    }
                    else if (userInput == "6")
                    {
                        _transactionHistoryService.DisplayTransactionHistory(UserSession.LoggedInUser);
                        Console.WriteLine("\nPress any key to continue ");
                        Console.ReadKey();
                    }
                    else if (userInput == "7")
                    {
                        Console.WriteLine("Thank you for banking with us.");
                        loggedIn = false; // End the session
                    }
                    else
                    {
                        Console.WriteLine("Invalid option, please try again.");
                    }
                }

            }
        }

        private void LandingPageMenu()
        {
            Console.WriteLine("Welcome to UrLedger Bank");
            Console.WriteLine("=========================");
            Console.WriteLine("Press 1 to Sign Up");
            Console.WriteLine("Press 2 to Login");
            Console.WriteLine("Press 3 to Exit");

        }

    }
}
