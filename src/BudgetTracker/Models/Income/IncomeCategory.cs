using System.ComponentModel.DataAnnotations;
using BudgetTracker.Models.Expenses;
using Microsoft.EntityFrameworkCore;

namespace BudgetTracker.Models.Income
{
    [Index(nameof(ExpenseCategory.CategoryName), IsUnique = true)]
    public class IncomeCategory
    {
        [Key]
        public int IncomeCategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}