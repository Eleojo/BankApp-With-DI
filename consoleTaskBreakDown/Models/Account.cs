using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading.Tasks;

namespace BankApp.Classes
{
    public class Account
    {
        public Account(Guid userId,decimal accountBalance, string accountNumber, string accountType)
        {
            //this.accountPassword = accountPassword;
            //this.accountBVN  = accountBVN;
            this.Id = new Guid();
            this.userId = userId;
            this.accountNumber = accountNumber;
            this.accountType = accountType;
            this.accountBalance = accountBalance;
            //this.note = note;
        }

       // public Guid accountBVN { get; set; }
        public string accountNumber { get; set; }

        public string accountType { get; set; }
        public decimal accountBalance { get; set; }

        //public string accountPassword {  get; set; }
        //public string note {  get; set; }
        public Guid userId { get; set; }
        public Guid Id { get; set; }
    }


}
