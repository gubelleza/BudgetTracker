using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BudgetTracker.Models.Budgets;
using BudgetTracker.Models.Enums;
using BudgetTracker.Models.Users;

namespace BudgetTracker.Models.Transactions
{
    public class Transaction
    {
        [Key]
        public long TransactionId { get; set; }
        public decimal Amount { get; set; }
        public DateTime TransactionDate { get; set; }
        public string BudgetMemberName { get; set; }
        
        [ForeignKey("TransactionCategory")]
        public int TransactionCategoryId { get; set; }
        public virtual TransactionCategory TransactionCategory { get; set; }
        
        [ForeignKey("Budget")]
        public Guid BudgetId { get; set; }
        public Budget Budget { get; set; }
    }
}