using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BudgetTracker.Data;
using BudgetTracker.Models.Transactions;
using BudgetTracker.Models.ViewModels;
using BudgetTracker.Services.Interfaces;
using BudgetTracker.Util.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace BudgetTracker.Services
{
    public class TransactionsService : ITransactionsService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        
        public TransactionsService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void DeleteTransaction(int id)
        {
            var transaction = _context.Transactions.FirstOrDefault(e => e.TransactionId == id);

            // TODO: PUT A NULL VERIFICATION HERE
            _context.Remove(transaction);
            _context.SaveChanges();
        }

        public TransactionsTableViewModel BuildTransactionsTableViewModel(Guid budgetId)
        {
            return new TransactionsTableViewModel
            {
                BudgetMembersNames = GetBudgetMembersNames(budgetId),
                CurrentCategories = GetTransactionsCategories(budgetId),
                Transactions = GetCurrentMonthTransactions(budgetId)
            };        
        }

        public EditCategoriesViewModel BuildEditCategoriesViewModel(Guid budgetId)
        {
            var categories = _context.TransactionsCategories
                .Where(c => c.BudgetId == budgetId).ToList();
            var editVm = new EditCategoriesViewModel
            {
                Categories = categories,
                BudgetId = budgetId
            };
            editVm.PopulateCategoriesToDelete(categories.Count);
            return editVm;
        }
        
        public AddTransactionViewModel BuildAddTransactionViewModel(Guid budgetId)
        {
            return new AddTransactionViewModel
            {
                BudgetMembersNames = GetBudgetMembersNames(budgetId),
                CurrentCategories = GetTransactionsCategories(budgetId),
                BudgetId = budgetId
            };
        }

        public AddCategoriesViewModel BuildAddCategoriesViewModel(
            int emptyCategoriesQuantity, Guid budgetId)
        {
            var categories = new List<TransactionCategory>();

            for (var i = 0; i < emptyCategoriesQuantity; i++)
                categories.Add(new TransactionCategory());

            return new AddCategoriesViewModel
            {
                Categories = categories,
                BudgetId = budgetId
            };
        }
        
        public List<string> GetBudgetMembersNames(Guid budgetId)
        {
            return _context.BudgetMembers
                .Where(m => m.BudgetId == budgetId)
                .Select(m => m.User.Username)
                .ToList();
        }
        
        public List<TransactionCategory> GetTransactionsCategories(Guid budgetId)
        {
            return _context
                .TransactionsCategories
                .Where(c => c.BudgetId == budgetId)
                .OrderBy(ec => ec.CategoryName)
                .ToList();
        }

        public bool AddCategories(List<TransactionCategory> transactionsCategories)
        {
            _context.AddRange(transactionsCategories.Where(c => !string.IsNullOrEmpty(c.CategoryName)));
            _context.SaveChanges();

            return true;
        }
        
        public List<IGrouping<string, Transaction>> GetCurrentMonthTransactions(Guid budgetId)
        {
            var transactions = _context
                .Transactions
                .Where(e => e.TransactionDate.Month == DateTime.Now.Month && e.BudgetId == budgetId)
                .Include(e => e.TransactionCategory)
                .OrderBy(e => e.TransactionDate)
                .ToList()
                .GroupBy(e => e.TransactionCategory.CategoryName);
                
            return transactions.ToList();
        }

        public bool EditTransaction(TransactionsTableViewModel transactionEditVm, ModelStateDictionary modelState)
        {
            var currentExpense = _context.Transactions.FirstOrDefault(
                e => e.TransactionId == transactionEditVm.TransactionCategoryId);

            if (currentExpense == null || !modelState.IsValid)
            {
                return false;
            }
            currentExpense = _mapper.Map(transactionEditVm, currentExpense);

            _context.Transactions.Update(currentExpense);
            _context.SaveChanges();

            return true;
        }

        public bool AddTransaction(AddTransactionViewModel addTransactionVm, ModelStateDictionary modelState)
        {
            if (modelState.IsValid)
            {
                var expense = _mapper.Map<Transaction>(addTransactionVm);
                _context.Transactions.Add(expense);
                _context.SaveChanges();
                return true;
            }

            return false;
        }

        public bool EditCategories(EditCategoriesViewModel editCategoriesVm)
        {
            if (DeleteCategoriesIfAny(editCategoriesVm) || UpdateCategoriesIfAny(editCategoriesVm))
            {
                _context.SaveChanges();
                return true;
            }
            return true;
        }

        private bool UpdateCategoriesIfAny(EditCategoriesViewModel editCategoriesVm)
        {
            var categoriesToUpdate = editCategoriesVm
                .Categories
                .Where(c => !string.IsNullOrEmpty(c.CategoryName) 
                            && editCategoriesVm.DeleteCategories.Any(dc => 
                                dc.Id == c.CategoryId && !dc.Deleted))
                .ToList();
            
            if (categoriesToUpdate.Any())
            {
                _context.TransactionsCategories.UpdateRange(categoriesToUpdate);
                return true;
            }

            return false;
        }
        
        private bool DeleteCategoriesIfAny(EditCategoriesViewModel editCategoriesVm)
        {
            var deleteIds = editCategoriesVm
                .DeleteCategories
                .Where(c => c.Deleted)
                .Select(c => c.Id)
                .ToList();
            
            if (deleteIds.Any())
            {
                var categoriesToDelete = editCategoriesVm
                    .Categories
                    .Where(c => deleteIds.Contains(c.CategoryId));
                _context.TransactionsCategories.RemoveRange(categoriesToDelete);
                return true;
            }

            return false;
        }
    }
}