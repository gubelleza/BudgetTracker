using BudgetTracker.Models.Users;
using BudgetTracker.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BudgetTracker.Services.Interfaces
{
    public interface IUsersService
    {
        public bool CreateUser(CreateUserViewModel createUserVm, ModelStateDictionary modelState);
        public bool Login(LoginViewModel loginVm, ModelStateDictionary modelState, ISession session);
        public bool UsernameExists(string username);
        public bool EmailExists(string email);
        public void Logout(ISession session);
    }
}