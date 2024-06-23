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
        void WithdrawMoney(User sessionUser);
        void DepositMoney(User sessionUser);
        void Transfer(User sessionUser);

    }
}
