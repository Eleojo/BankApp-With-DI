using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Services.AccountServices
{
    public interface IAccountService
    {
        void OpenAccount(Guid userId);
    }
}
