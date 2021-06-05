using System;
using System.Collections.Generic;
using System.Linq;
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

        public ExpensesService(ApplicationDbContext context)
        {
            _context = context;
        }

        public void DeleteExpense(int id)
        {
            var expense = _context.Expenses.FirstOrDefault(e => e.ExpenseId == id);

            // TODO: PUT A NULL VERIFICATION HERE
            _context.Remove(expense);
            _context.SaveChanges();
        }

        public IExpenseInputViewModel BuildExpenseInputViewModel()
        {
            return new ExpenseInputViewModel
            {
                BudgetMembersNames = GetBudgetMembersNames(),
                CurrentCategories = GetExpenseCategories()
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
        
        public bool AddExpense(Expense expense, ModelStateDictionary modelState)
        {
            if (modelState.IsValid)
            {
                _context.Expenses.Add(expense);
                _context.SaveChanges();
                return true;
            }

            return false;
        }
    }
}