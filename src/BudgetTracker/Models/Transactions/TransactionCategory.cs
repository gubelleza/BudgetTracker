using BudgetTracker.Models.Budgets;
using BudgetTracker.Models.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BudgetTracker.Models.Transactions
{
    [Microsoft.EntityFrameworkCore.Index(nameof(TransactionCategory.CategoryName), IsUnique = true)]
    public class TransactionCategory
    {
        [Key]
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public Recurrence Recurrence { get; set; }
        public TransactionType TransactionType { get; set; }

        [ForeignKey("Budget")]
        public Guid BudgetId { get; set; }
        public Budget Budget { get; set; }
    }
}