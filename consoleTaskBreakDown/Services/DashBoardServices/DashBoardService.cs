using BankApp.Classes;
using BankApp.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Services.DashBoardServices
{
    internal class DashBoardService : IDashBoardService
    {
        public void ApplicationDashBoard(User loggedInUser)
        {
            Console.WriteLine($"Welcome, {Validations.Capitalize(loggedInUser.FirstName)}");
            Console.WriteLine("\nWhat would you like to do today\n");
            Console.WriteLine("Press 1 to Withdraw money.");
            Console.WriteLine("Press 2 to Deposit money.");
            Console.WriteLine("Press 3 to Display Account info.");
            Console.WriteLine("Press 4 to transfer.");
            Console.WriteLine("Press 5 to Show All Accounts.");
            Console.WriteLine("Press 6 to Show User Transaction History.");
            Console.WriteLine("Press 7 to Exit.");
        }

    }
}
