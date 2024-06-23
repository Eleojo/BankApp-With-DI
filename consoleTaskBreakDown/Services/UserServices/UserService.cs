using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankApp.Classes;
using BankApp.Database;
using BankApp.Utilities;
using ConsoleTableExt;

namespace BankApp.Services.UserServices
{
    public class UserService : IUserService
    {
        public Guid RegisterUser()
        {
            var id = Guid.NewGuid();

            string firstName = Validations.GetValidInput("Enter your FirstName:", Validations.IsValidName, "Name cannot be empty or contain digits. Please try again.");
            string lastName = Validations.GetValidInput("Enter your LastName:", Validations.IsValidName, "Name cannot be empty or contain digits. Please try again.");
            string email = Validations.GetValidInput("Enter your email:", Validations.IsValidEmail, "Incorrect email address. Please try again.");
            string password = Validations.GetValidInput("Enter your password:", Validations.IsValidPassword, "Password must contain at least 6 characters, one uppercase letter, one digit, and one special character. Please try again.");

            User newUser = new User(id, Validations.Capitalize(firstName), Validations.Capitalize(lastName), email, password);

            using (BankApp_DbContext db = new BankApp_DbContext())
            {
                List<User> users = db.GetAllEntities<User>();
                List<Account> accounts = db.GetAllEntities<Account>();

                // Find the user by email
                User foundUser = users.FirstOrDefault(user => user.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
                //bool accountyTypeExists = accounts.Any(account => account.userId.Equals(id) || account => account.accountType.Equals();
                if (foundUser != null)
                {
                    // User already exists, access their account
                    Console.WriteLine("User already exists....\n");
                    return foundUser.Id;

                    //Thread.Sleep(2000); // Sleep for 2000 milliseconds (2 seconds)

                }
                else
                {
                    // User does not exist, register the new user
                    db.AddEntity(newUser);
                    Console.WriteLine("User Registered Successfully");
                }
            }

            return id;
        }




    }
}

