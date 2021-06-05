using System.Collections.Generic;
using System.Linq;
using BudgetTracker.Models.Expenses;
using BudgetTracker.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BudgetTracker.Services.Interfaces
{
    public interface IExpensesService
    {
        public List<IGrouping<string, Expense>> GetCurrentMonthExpenses();
        public bool AddExpense(Expense expense, ModelStateDictionary modelState);
        public List<ExpenseCategory> GetExpenseCategories();
        public List<string> GetBudgetMembersNames();
        public IExpenseInputViewModel BuildExpenseInputViewModel();
        public void DeleteExpense(int id);

    }
}