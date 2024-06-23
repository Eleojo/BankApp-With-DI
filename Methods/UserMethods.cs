using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using consoleTaskBreakDown.Classes;
using consoleTaskBreakDown.Database;

namespace consoleTaskBreakDown.Methods
{
    public class UserMethods
    {

        public void Register(string firstName, string lastName, string email, string password)
        {
            var id = Guid.NewGuid();
            User newUser = new User(id, firstName, lastName, email, password);
            Container.UserDatabase.Add(newUser);
        }



    }
}
