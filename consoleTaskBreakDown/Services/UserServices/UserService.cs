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
        public async Task RegisterUser()
        {
            //var id = Guid.NewGuid();

            string firstName = Validations.GetValidInput("Enter your FirstName:", Validations.IsValidName, "Name cannot be empty or contain digits. Please try again.");
            string lastName = Validations.GetValidInput("Enter your LastName:", Validations.IsValidName, "Name cannot be empty or contain digits. Please try again.");
            string email = Validations.GetValidInput("Enter your email:", Validations.IsValidEmail, "Incorrect email address. Please try again.");
            string password = Validations.GetValidInput("Enter your password:", Validations.IsValidPassword, "Password must contain at least 6 characters, one uppercase letter, one digit, and one special character. Please try again.");

            User newUser = new User(Validations.Capitalize(firstName), Validations.Capitalize(lastName), email, password);

            using (BankAppDbContext db = new BankAppDbContext())
            {
                var users = await db.GetAllEntities<User>();
                var accounts = await db.GetAllEntities<Account>();

                // Find the user by email
                User foundUser = users.FirstOrDefault(user => user.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
                //bool accountyTypeExists = accounts.Any(account => account.userId.Equals(id) || account => account.accountType.Equals();
                if (foundUser != null)
                {
                    // User already exists, access their account
                    Console.WriteLine("User already exists....\n");
                    //return foundUser.Id;

                    //Thread.Sleep(2000); // Sleep for 2000 milliseconds (2 seconds)

                }
                else
                {
                    // User does not exist, register the new user
                    db.AddEntity(newUser);

                    // instantiate and persist newUser in preparation for account creation
                    //UserSession userSession = new UserSession();
                    UserSession.RegisteredUser = newUser;

                    Console.WriteLine("User Registered Successfully");
                }
            }

            //return id;
        }




    }
}

