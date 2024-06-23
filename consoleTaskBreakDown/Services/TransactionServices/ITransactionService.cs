using BankApp.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Services.TransactionServices
{
    public interface ITransactionService
    {
        Task WithdrawMoney(User sessionUser);
        Task DepositMoney(User sessionUser);
        Task Transfer(User sessionUser);

    }
}
