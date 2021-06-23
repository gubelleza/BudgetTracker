using BudgetTracker.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BudgetTracker.Services.Interfaces
{
    public interface IAuthenticationService
    {
        public void Logout(ISession session);
        public bool Login(LoginViewModel loginVm, ModelStateDictionary modelState, ISession session);
    }
}