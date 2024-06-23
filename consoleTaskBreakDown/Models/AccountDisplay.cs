using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Classes
{
    internal class AccountDisplay
    {
       
        public string FullName { get; set; }
        public string AccountNumber { get; set; }
        public string AccountType { get; set; }
        public decimal AccountBalance { get; set; }
        public Guid UserId { get; set; }
        
    }
}
