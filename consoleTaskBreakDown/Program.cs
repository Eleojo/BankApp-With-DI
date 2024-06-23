using BankApp.Classes;
using BankApp.Database;
using BankApp.Services.AccountServices;
using BankApp.Services.AuthServices;
using BankApp.Services.DashBoardServices;
using BankApp.Services.TransactionHistoryServices;
using BankApp.Services.TransactionServices;
using BankApp.Services.UserServices;
using BankApp.Utilities;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace BankApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var services = new ServiceCollection();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IDashBoardService, DashBoardService>();
            services.AddScoped<ITransactionService, TransactionService>();
            services.AddScoped<ITransactionHistoryService, TransactionHistoryService>();


            services.AddSingleton<Run>();
            var serviceProvider = services.BuildServiceProvider();
            var run = serviceProvider.GetRequiredService<Run>();

            run.StartApp();
        }
    }
}

