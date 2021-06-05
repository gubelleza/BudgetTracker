using BudgetTracker.Models.Enums;
using BudgetTracker.Models.Expenses;
using System;
using System.Collections.Generic;

namespace BudgetTracker.Models.ViewModels
{
    public interface IExpenseInputViewModel
    {
        public Recurrence Recurrence { get; set; }
        public decimal AmountPaid { get; set; }
        public DateTime PaidAt { get; set; }
        public string BudgetMemberName { get; set; }
        public int ExpenseCategoryId { get; set; }
        public List<ExpenseCategory> CurrentCategories { get; set; }
        public List<string> BudgetMembersNames { get; set; }
        public Expense ToExpense { get; }
    }
}

