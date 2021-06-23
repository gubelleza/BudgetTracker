using System.Linq;
using AutoMapper;
using BudgetTracker.Data;
using BudgetTracker.Models.Users;
using BudgetTracker.Models.ViewModels;
using BudgetTracker.Services.Interfaces;
using BudgetTracker.Util.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using static BudgetTracker.Util.Constants.ModelStateErrors;

namespace BudgetTracker.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly ApplicationDbContext _context;
        
        public AuthenticationService(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public bool Login(LoginViewModel loginVm, ModelStateDictionary modelState, ISession session)
        {
            if (!modelState.IsValid)
                return false;

            var user = _context.Users.FirstOrDefault(
                u => u.Username == loginVm.UsernameOrEmail || u.Email == loginVm.UsernameOrEmail);

            if (user == null || !HasValidPassword(loginVm, user))
            {
                modelState.AddModelError(SUMMARY, WRONG_USERNAME_PASSWORD);
                return false;
            }

            session.SetUserId(user.UserId);
            session.SetUsername(user.Username);
            return true;
        }
        
        public void Logout(ISession session)
        {
            session.RemoveUserData();
        }

        private bool HasValidPassword(LoginViewModel loginVm, User storedUser)
        {
            return BCrypt.Net.BCrypt.Verify(loginVm.Password, storedUser.Password);
        }
    }
}