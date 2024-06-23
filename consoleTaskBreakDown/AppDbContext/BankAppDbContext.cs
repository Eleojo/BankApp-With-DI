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

    public class BankAppDbContext : DbContext
    {
        public  DbSet<User> Users { get; set; }
        public  DbSet<Account> Accounts { get; set; }
        public  DbSet<TransactionHistory> Transactions { get; set; }
        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("MyBankApp_Db");
        }

        // Add User or Account type to database
        public async Task AddEntity<T>(T entity) where T : class
        {
            using (var context = new BankAppDbContext())
            {
                await context.Set<T>().AddAsync(entity); // instead of context.Users.Add(user) or context.Accounts.Add(account)[instead of creating two seperate but similar functions]
                await context.SaveChangesAsync();
                
            }
        }
        // Retrieve all users or accounts from Database
        public async Task<List<T>> GetAllEntities<T>() where T : class
        {
            using (var context = new BankAppDbContext())
            {
                return await context.Set<T>().ToListAsync(); // Instead of context.Users.Add
            }
        }

        public void UpdateEntities<T>(List<T> updatedEntities) where T : class
        {
            using (var context = new BankAppDbContext())
            {
                foreach (var entity in updatedEntities)
                {
                    context.Set<T>().Update(entity);
                }
                context.SaveChanges();
            }
        }

        public List<TransactionHistory> GetUserTransactions(Guid accountId)
        {
            return Transactions.Where(t => t.AccountId == accountId).ToList();
        }
    }
}
