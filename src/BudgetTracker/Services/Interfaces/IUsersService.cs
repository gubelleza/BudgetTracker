using BudgetTracker.Models.Users;
using BudgetTracker.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BudgetTracker.Services.Interfaces
{
    public interface IUsersService
    {
        public bool CreateUser(CreateUserViewModel createUserVm, ModelStateDictionary modelState);
        public bool UsernameExists(string username);
        public bool EmailExists(string email);

    }
}