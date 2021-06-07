using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BudgetTracker.Models.Enums;
using BudgetTracker.Models.Expenses;

namespace BudgetTracker.Models.ViewModels
{
    public class ExpenseEditViewModel
    {
        public int ExpenseId { get; set; }
        public Recurrence Recurrence { get; set; }
        
        [Range(1.0, Double.MaxValue)]
        public decimal? AmountPaid { get; set; }
        
        public DateTime? PaidAt { get; set; }
        public string BudgetMemberName { get; set; }
        public int? ExpenseCategoryId { get; set; }
        public List<ExpenseCategory> CurrentCategories { get; set; }
        public List<string> BudgetMembersNames { get; set; }
    }
}