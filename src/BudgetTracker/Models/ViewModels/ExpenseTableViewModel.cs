using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using BudgetTracker.Models.Enums;
using BudgetTracker.Models.Expenses;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BudgetTracker.Models.ViewModels
{
    public class ExpenseTableViewModel
    {
        #region DisplayExpenses
        [BindNever]
        public List<IGrouping<string, Expense>> Expenses { get; set; }
        
        [BindNever]
        public bool HasExpenses => Expenses != null && Expenses.Any();
        #endregion
        
        #region ExpenseInput
        public int ExpenseId { get; set; }
        public Recurrence Recurrence { get; set; }
        
        [Range(1.0, Double.MaxValue, ErrorMessage = "Amount paid can't be zero")]
        public decimal? AmountPaid { get; set; }
        
        public DateTime? PaidAt { get; set; }
        public string BudgetMemberName { get; set; }
        public int? ExpenseCategoryId { get; set; }
        
        public Guid BudgetId { get; set; }
        #endregion
        
        #region ExpenseFormAttributes
        [BindNever]
        public List<ExpenseCategory> CurrentCategories { get; set; }
        
        [BindNever]
        public List<string> BudgetMembersNames { get; set; }
        #endregion
    }
}