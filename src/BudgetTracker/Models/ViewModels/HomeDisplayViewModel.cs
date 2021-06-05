using System.Collections.Generic;
using System.Linq;
using BudgetTracker.Models.Expenses;

namespace BudgetTracker.Models.ViewModels
{
    public class HomeDisplayViewModel
    {
        public List<IGrouping<string, Expense>> Expenses { get; set; }
        public bool HasExpenses => Expenses != null && Expenses.Any();
        public IExpenseInputViewModel EditExpenseViewModel { get; set; }
    }
}