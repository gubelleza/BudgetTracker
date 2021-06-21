using System;
using System.Collections.Generic;
using System.Linq;
using BudgetTracker.Data;
using BudgetTracker.Models.Budgets;
using BudgetTracker.Models.Transactions;
using BudgetTracker.Models.ViewModels;
using BudgetTracker.Services.Interfaces;
using BudgetTracker.Util.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace BudgetTracker.Services
{
    public class BudgetService : IBudgetService
    {
        private readonly ApplicationDbContext _context;

        public BudgetService(ApplicationDbContext context)
        {
            _context = context;
        }

        public UserBudgetsViewModel BuildUserBudgetsViewModel(ISession session)
        {
            return new UserBudgetsViewModel
            {
                Budgets = ListUserBudgets(session)
            };
        }

        public List<Budget> ListUserBudgets(ISession session)
        {
            var userBudgets = _context
                .BudgetMembers
                .Where(bm => bm.UserId == session.GetUserId())
                .Include(bm => bm.Budget)
                .Select(bm => bm.Budget)
                .ToList();
            return userBudgets;
        }

        public bool CreateBudget(UserBudgetsViewModel userBudgetsVm, ISession session, ModelStateDictionary modelState)
        {
            if (!modelState.IsValid)
                return false;
            
            var budget = new Budget{ Name = userBudgetsVm.BudgetName };
            _context.Budgets.Add(budget);
            _context.SaveChanges();

            _context.BudgetMembers.Add(new BudgetMember
            {
                BudgetId = budget.BudgetId,
                UserId = session.GetUserId().Value
            });
            _context.SaveChanges();
            return true;
        }

        public bool AddMember(string username, Guid budgetId, ModelStateDictionary modelState)
        {
            if (!modelState.IsValid)
                return false;

            var userId = _context.Users
                .Where(u => u.Username == username)
                .Select(u => u.UserId).FirstOrDefault();

            if (userId == default)
            {
                modelState.AddModelError("NewMemberUserName", "User don't exists");
                return false;
            }

            var isMember = _context.BudgetMembers.Any(m => m.UserId == userId);
            if (isMember)
            {
                modelState.AddModelError("NewMemberUserName", $"{username} is already a member");
                return false;
            }
            
            _context.BudgetMembers.Add(new BudgetMember
            {
                UserId = userId,
                BudgetId = budgetId
            });
            _context.SaveChanges();

            return true;
        }

        public bool DeleteBudget(Guid budgetId)
        {
            if (budgetId == default)
                return false;

            var budgetMember = _context
                .BudgetMembers
                .Include(bm => bm.Budget)
                .Where(bm => bm.BudgetId == budgetId)
                .FirstOrDefault();

            if (budgetMember == null)
                return false;

            _context.BudgetMembers.Remove(budgetMember);
            _context.SaveChanges();

            return true;
        }
    }
}