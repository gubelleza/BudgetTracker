using System.Collections.Generic;
using BudgetTracker.Models.Budgets;
using BudgetTracker.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BudgetTracker.Services.Interfaces
{
    public interface IBudgetService
    {
        public List<Budget> ListUserBudgets(ISession session);
        public UserBudgetsViewModel BuildUserBudgetsViewModel(ISession session);
        public bool CreateBudget(UserBudgetsViewModel userBudgetsVm, ISession session, ModelStateDictionary modelState);
    }
}