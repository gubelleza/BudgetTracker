using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BudgetTracker.Data;
using BudgetTracker.Models.Expenses;
using BudgetTracker.Models.ViewModels;
using BudgetTracker.Services.Interfaces;
using BudgetTracker.Util;
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

        public ExpenseEditViewModel BuildEditExpenseViewModel()
        {
            return new ExpenseEditViewModel
            {
                BudgetMembersNames = GetBudgetMembersNames(),
                CurrentCategories = GetExpenseCategories()
            };        
        }
        
        public CreateExpenseViewModel BuildCreateExpenseViewModel()
        {
            return new CreateExpenseViewModel
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

        public bool EditExpense(ExpenseEditViewModel expenseEditVm, ModelStateDictionary modelState)
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
    }
}