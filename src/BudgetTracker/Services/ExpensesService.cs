using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BudgetTracker.Data;
using BudgetTracker.Models.Expenses;
using BudgetTracker.Models.ViewModels;
using BudgetTracker.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace BudgetTracker.Services
{
    public class ExpensesService : IExpensesService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        
        public ExpensesService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void DeleteExpense(int id)
        {
            var expense = _context.Expenses.FirstOrDefault(e => e.ExpenseId == id);

            // TODO: PUT A NULL VERIFICATION HERE
            _context.Remove(expense);
            _context.SaveChanges();
        }

        public ExpenseTableViewModel BuildExpenseTableViewModel()
        {
            return new ExpenseTableViewModel
            {
                BudgetMembersNames = GetBudgetMembersNames(),
                CurrentCategories = GetExpenseCategories(),
                Expenses = GetCurrentMonthExpenses()
            };        
        }

        public EditCategoriesViewModel BuildEditCategoriesViewModel()
        {
            var categories = _context.ExpenseCategories.ToList();
            var editVm = new EditCategoriesViewModel
            {
                Categories = categories,
            };
            editVm.PopulateCategoriesToDelete(categories.Count);
            return editVm;
        }
        
        public CreateExpenseViewModel BuildCreateExpenseViewModel()
        {
            return new CreateExpenseViewModel
            {
                BudgetMembersNames = GetBudgetMembersNames(),
                CurrentCategories = GetExpenseCategories()
            };
        }

        public AddCategoriesViewModel<ExpenseCategory> BuildAddCategoriesViewModel(int emptyCategoriesQuantity)
        {
            var categories = new List<ExpenseCategory>();

            for (var i = 0; i < emptyCategoriesQuantity; i++)
                categories.Add(new ExpenseCategory());

            return new AddCategoriesViewModel<ExpenseCategory>
            {
                Categories = categories
            };
        }
        
        public List<string> GetBudgetMembersNames()
        {
            return _context.Users.Select(u => u.Username).ToList();
        }
        
        public List<ExpenseCategory> GetExpenseCategories()
        {
            return _context
                .ExpenseCategories
                .OrderBy(ec => ec.CategoryName)
                .ToList();
        }

        public bool AddCategories(List<ExpenseCategory> expenseCategories)
        {
            _context.AddRange(expenseCategories);
            _context.SaveChanges();
            return true;
        }
        
        public List<IGrouping<string, Expense>> GetCurrentMonthExpenses()
        {
            var expenses = _context
                .Expenses
                .Where(e => e.PaidAt.Month == DateTime.Now.Month)
                .Include(e => e.ExpenseCategory)
                .OrderBy(e => e.PaidAt)
                .ToList()
                .GroupBy(e => e.ExpenseCategory.CategoryName);
                
            return expenses.ToList();
        }

        public bool EditExpense(ExpenseTableViewModel expenseEditVm, ModelStateDictionary modelState)
        {
            var currentExpense = _context.Expenses.FirstOrDefault(e => e.ExpenseId == expenseEditVm.ExpenseId);

            if (currentExpense == null || !modelState.IsValid)
            {
                return false;
            }
            currentExpense = _mapper.Map(expenseEditVm, currentExpense);

            _context.Expenses.Update(currentExpense);
            _context.SaveChanges();

            return true;
        }

        public bool AddExpense(CreateExpenseViewModel expenseInputVm, ModelStateDictionary modelState)
        {
            if (modelState.IsValid)
            {
                var expense = _mapper.Map<Expense>(expenseInputVm);
                _context.Expenses.Add(expense);
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
                _context.ExpenseCategories.UpdateRange(categoriesToUpdate);
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
                _context.ExpenseCategories.RemoveRange(categoriesToDelete);
                return true;
            }

            return false;
        }
    }
}