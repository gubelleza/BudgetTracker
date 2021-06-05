using BudgetTracker.Models.Enums;
using BudgetTracker.Models.Expenses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetTracker.Models.ViewModels
{
    public class ExpenseInputViewModel : IExpenseInputViewModel
    {
        public Recurrence Recurrence { get; set; }
        public decimal AmountPaid { get; set; }
        public DateTime PaidAt { get; set; }
        public string BudgetMemberName { get; set; }
        public int ExpenseCategoryId { get; set; }
        public List<ExpenseCategory> CurrentCategories { get; set; }
        public List<string> BudgetMembersNames { get; set; }

        public Expense ToExpense => new Expense
        {
            Recurrence = Recurrence,
            AmountPaid = AmountPaid,
            ExpenseCategoryId = ExpenseCategoryId,
            PaidAt = PaidAt,
            BudgetMemberName = BudgetMemberName
        };
    }
}
