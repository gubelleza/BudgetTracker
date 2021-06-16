using BudgetTracker.Models.Budgets;
using BudgetTracker.Models.Expenses;
using BudgetTracker.Models.Income;
using BudgetTracker.Models.Users;
using Microsoft.EntityFrameworkCore;

namespace BudgetTracker.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Expense> Expenses { get; set; } 
        public DbSet<ExpenseCategory> ExpenseCategories { get; set; } 
        public DbSet<Income> Incomes { get; set; } 
        public DbSet<IncomeCategory> IncomeCategories { get; set; } 
        public DbSet<User> Users { get; set; }
        public DbSet<Budget> Budgets { get; set; }
        public DbSet<BudgetMember> BudgetMembers { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}