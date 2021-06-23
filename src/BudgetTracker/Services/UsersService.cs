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
    public class UsersService : IUsersService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        
        public UsersService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public bool UsernameExists(string username)
        {
            return _context.Users.Select(u => u.Username).FirstOrDefault(u => u == username) != null;
        }

        public bool EmailExists(string email)
        {
            return _context.Users.Select(u => u.Email).FirstOrDefault(e => e == email) != null;
        }
        
        public bool CreateUser(CreateUserViewModel createUserVm, ModelStateDictionary modelState)
        {
            ValidateIfUsernameExists(createUserVm, modelState);
            ValidateIfEmailExists(createUserVm, modelState);
            ValidateIfPasswordsMatch(createUserVm, modelState);
            ValidateIfEmailsMatch(createUserVm, modelState);
            
            if (!modelState.IsValid)
                return false;

            var user = _mapper.Map<User>(createUserVm);
            _context.Users.Add(user);
            _context.SaveChanges();
            
            createUserVm.ResetPassword();
            return true;
        }
        
        private void ValidateIfUsernameExists(CreateUserViewModel createUserVm, ModelStateDictionary modelState)
        {
            if (UsernameExists(createUserVm.Username))
                modelState.AddModelError(nameof(createUserVm.Username), DATA_ALREADY_EXISTS);
        }
        
        private void ValidateIfEmailExists(CreateUserViewModel createUserVm, ModelStateDictionary modelState)
        {
            if (EmailExists(createUserVm.Email))
                modelState.AddModelError(nameof(createUserVm.Email), DATA_ALREADY_EXISTS);
        }

        private void ValidateIfPasswordsMatch(CreateUserViewModel createUserVm, ModelStateDictionary modelState)
        {
            if (!createUserVm.PasswordsMatch)
            {
                modelState.AddModelError(nameof(createUserVm.ConfirmPassword), PASSWORDS_MUST_MATCH);
            }
        }
        
        private void ValidateIfEmailsMatch(CreateUserViewModel createUserVm, ModelStateDictionary modelState)
        {
            if (!createUserVm.EmailsMatch)
            {
                modelState.AddModelError(nameof(createUserVm.ConfirmEmail), EMAILS_MUST_MATCH);
            }
        }
    }
}