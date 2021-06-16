using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BudgetTracker.Models.Budgets;
using BudgetTracker.Models.Enums;

namespace BudgetTracker.Models.Expenses
{
    [Microsoft.EntityFrameworkCore.Index(nameof(ExpenseCategory.CategoryName), IsUnique = true)]
    public class ExpenseCategory
    {
        [Key]
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public Recurrence Recurrence { get; set; }
        
        [ForeignKey("Budget")]
        public Guid BudgetId { get; set; }
        public Budget Budget { get; set; }
    }
}