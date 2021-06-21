using BudgetTracker.Models.Budgets;
using BudgetTracker.Models.Transactions;
using BudgetTracker.Models.Users;
using Microsoft.EntityFrameworkCore;

namespace BudgetTracker.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Transaction> Transactions { get; set; } 
        public DbSet<TransactionCategory> TransactionsCategories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Budget> Budgets { get; set; }
        public DbSet<BudgetMember> BudgetMembers { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}