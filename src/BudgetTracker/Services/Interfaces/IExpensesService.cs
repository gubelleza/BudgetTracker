using System;
using System.Collections.Generic;
using System.Linq;
using BudgetTracker.Models.Expenses;
using BudgetTracker.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BudgetTracker.Services.Interfaces
{
    public interface IExpensesService
    {
        public List<IGrouping<string, Expense>> GetCurrentMonthExpenses(Guid budgetId);
        public bool AddExpense(
            CreateExpenseViewModel expenseInputViewModel, ModelStateDictionary modelState, ISession session);
        public bool EditExpense(ExpenseTableViewModel expenseInput, ModelStateDictionary modelState);
        public List<ExpenseCategory> GetExpenseCategories(Guid budgetId);
        public List<string> GetBudgetMembersNames(Guid budgetId);
        public CreateExpenseViewModel BuildCreateExpenseViewModel(ISession session);
        public ExpenseTableViewModel BuildExpenseTableViewModel(Guid budgetId);
        public void DeleteExpense(int id);
        public AddCategoriesViewModel<ExpenseCategory> BuildAddCategoriesViewModel(int emptyCategoriesQuantity);
        public bool AddCategories(List<ExpenseCategory> expenseCategories);
        public EditCategoriesViewModel BuildEditCategoriesViewModel(ISession session);
        public bool EditCategories(EditCategoriesViewModel editCategoriesVm);
    }
}