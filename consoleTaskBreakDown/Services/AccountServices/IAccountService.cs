using BankApp.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Services.AccountServices
{
    public interface IAccountService
    {
        Task OpenAccount(Guid userId);
        Task DisplayAccountInfo(User sessionUser);
        void ShowAllDb<T>(List<T> obj) where T : class;
    }
}
