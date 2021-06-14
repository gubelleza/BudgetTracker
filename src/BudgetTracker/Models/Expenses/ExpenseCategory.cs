using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BudgetTracker.Models.Enums;
using BudgetTracker.Models.Expenses;
using Microsoft.EntityFrameworkCore;

namespace BudgetTracker.Models.Expenses
{
    [Index(nameof(ExpenseCategory.CategoryName), IsUnique = true)]
    public class ExpenseCategory
    {
        [Key]
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public Recurrence Recurrence { get; set; }
    }
}