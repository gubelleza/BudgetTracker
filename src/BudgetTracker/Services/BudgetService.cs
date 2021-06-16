using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using BudgetTracker.Data;
using BudgetTracker.Models.Budgets;
using BudgetTracker.Models.ViewModels;
using BudgetTracker.Services.Interfaces;
using BudgetTracker.Util.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;

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
    }
}