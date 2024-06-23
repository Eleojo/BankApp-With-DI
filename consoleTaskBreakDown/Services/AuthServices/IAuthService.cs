using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Services.AuthServices
{
    public interface IAuthService
    {
        string ReadPassword();
        bool AuthenticateUser(string email, string password);
    }
}
