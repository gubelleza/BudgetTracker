using System;
using System.Collections.Generic;
using System.Linq;
using BudgetTracker.Models.Transactions;
using BudgetTracker.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BudgetTracker.Services.Interfaces
{
    public interface ITransactionsService
    {
        public List<IGrouping<string, Transaction>> GetCurrentMonthTransactions(Guid budgetId);
        public bool AddTransaction(
            AddTransactionViewModel expenseInputViewModel, ModelStateDictionary modelState);
        public bool EditTransaction(TransactionsTableViewModel editTransactionVm, ModelStateDictionary modelState);
        public List<TransactionCategory> GetTransactionsCategories(Guid budgetId);
        public List<string> GetBudgetMembersNames(Guid budgetId);
        public AddTransactionViewModel BuildAddTransactionViewModel(Guid budgetId);
        public TransactionsTableViewModel BuildTransactionsTableViewModel(Guid budgetId);
        public void DeleteTransaction(int id);
        public AddCategoriesViewModel BuildAddCategoriesViewModel(
            int emptyCategoriesQuantity, Guid budgetId);
        public bool AddCategories(List<TransactionCategory> expenseCategories);
        public EditCategoriesViewModel BuildEditCategoriesViewModel(Guid budgetId);
        public bool EditCategories(EditCategoriesViewModel editCategoriesVm);
    }
}