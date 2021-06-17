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
            CreateExpenseViewModel expenseInputViewModel, ModelStateDictionary modelState);
        public bool EditExpense(ExpenseTableViewModel expenseInput, ModelStateDictionary modelState);
        public List<ExpenseCategory> GetExpenseCategories(Guid budgetId);
        public List<string> GetBudgetMembersNames(Guid budgetId);
        public CreateExpenseViewModel BuildCreateExpenseViewModel(Guid budgetId);
        public ExpenseTableViewModel BuildExpenseTableViewModel(Guid budgetId);
        public void DeleteExpense(int id);
        public AddCategoriesViewModel<ExpenseCategory> BuildAddCategoriesViewModel(
            int emptyCategoriesQuantity, Guid budgetId);
        public bool AddCategories(List<ExpenseCategory> expenseCategories);
        public EditCategoriesViewModel BuildEditCategoriesViewModel(Guid budgetId);
        public bool EditCategories(EditCategoriesViewModel editCategoriesVm);
    }
}