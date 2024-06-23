using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankApp.Classes;
using BankApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BankApp.Database
{
    //public static class BankDataBase_liteDb
    //{
    //    public static List<User> UserDatabase = new List<User>();
    //    public static List<Account> AccountsDatabase = new List<Account>();

    //}

    public class BankApp_DbContext : DbContext
    {
        public  DbSet<User> Users { get; set; }
        public  DbSet<Account> Accounts { get; set; }
        public  DbSet<TransactionHistory> Transactions { get; set; }
        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("MyBankApp_Db");
        }

        // Add User or Account type to database
        public void AddEntity<T>(T entity) where T : class
        {
            using (var context = new BankApp_DbContext())
            {
                context.Set<T>().Add(entity); // instead of context.Users.Add(user) or context.Accounts.Add(account)[instead of creating two seperate but similar functions]
                context.SaveChanges();
            }
        }
        // Retrieve all users or accounts from Database
        public List<T> GetAllEntities<T>() where T : class
        {
            using (var context = new BankApp_DbContext())
            {
                return context.Set<T>().ToList(); // Instead of context.Users.Add
            }
        }

        public void UpdateEntities<T>(List<T> updatedEntities) where T : class
        {
            using (var context = new BankApp_DbContext())
            {
                foreach (var entity in updatedEntities)
                {
                    context.Set<T>().Update(entity);
                }
                context.SaveChanges();
            }
        }

        public List<TransactionHistory> GetUserTransactions(Guid userId)
        {
            return Transactions.Where(t => t.UserId == userId).ToList();
        }
    }
}
