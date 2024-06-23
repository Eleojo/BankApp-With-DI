using BankApp.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Services.DashBoardServices
{
    public interface IDashBoardService
    {
        void ApplicationDashBoard(User loggedInUser);
    }
}
