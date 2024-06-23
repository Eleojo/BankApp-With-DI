using BankApp.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Services.TransactionHistoryServices
{
    public interface ITransactionHistoryService
    {
        Task DisplayTransactionHistory(User sessionUser);
    }
}
