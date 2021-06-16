using BudgetTracker.Models.Enums;
using BudgetTracker.Models.Expenses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetTracker.Models.ViewModels
{
    public class CreateExpenseViewModel 
    {
        [Required]
        public Recurrence Recurrence { get; set; }
        
        [Required]
        public decimal? AmountPaid { get; set; }
        
        [Required]
        public DateTime? PaidAt { get; set; }
        
        [Required]
        [MinLength(3)]
        public string BudgetMemberName { get; set; }
        
        public int? ExpenseCategoryId { get; set; }
        
        public List<ExpenseCategory> CurrentCategories { get; set; }
        public List<string> BudgetMembersNames { get; set; }
    }
}
