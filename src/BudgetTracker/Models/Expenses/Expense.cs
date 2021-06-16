using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BudgetTracker.Models.Budgets;
using BudgetTracker.Models.Enums;
using BudgetTracker.Models.Users;

namespace BudgetTracker.Models.Expenses
{
    public class Expense
    {
        [Key]
        public long ExpenseId { get; set; }
        public decimal AmountPaid { get; set; }
        public DateTime PaidAt { get; set; }
        public string BudgetMemberName { get; set; }
        
        [ForeignKey("ExpenseCategory")]
        public int ExpenseCategoryId { get; set; }
        public virtual ExpenseCategory ExpenseCategory { get; set; }
        
        [ForeignKey("Budget")]
        public Guid BudgetId { get; set; }
        public Budget Budget { get; set; }
    }
}