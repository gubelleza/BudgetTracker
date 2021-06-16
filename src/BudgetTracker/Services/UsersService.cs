using System;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using BudgetTracker.Data;
using BudgetTracker.Models.Users;
using BudgetTracker.Models.ViewModels;
using BudgetTracker.Services.Interfaces;
using BudgetTracker.Util.Extensions;
using LinqKit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;

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
            if (!modelState.IsValid)
                return false;

            var user = _mapper.Map<User>(createUserVm);
            _context.Users.Add(user);
            _context.SaveChanges();
            
            createUserVm.ResetPassword();
            return true;
        }

        public void Logout(ISession session)
        {
            session.RemoveUserData();
        }
        
        public bool Login(LoginViewModel loginVm, ModelStateDictionary modelState, ISession session)
        {
            if (!modelState.IsValid)
                return false;

            var user = _context.Users.FirstOrDefault(
                u => u.Username == loginVm.UsernameOrEmail || u.Email == loginVm.UsernameOrEmail);

            if (user == null || !HasValidPassword(loginVm, user))
            {
                modelState.AddModelError("summary", "Wrong username or password");
                return false;
            }

            session.SetUserId(user.UserId);
            session.SetUsername(user.Username);
            return true;
        }

        private bool HasValidPassword(LoginViewModel loginVm, User storedUser)
        {
            return BCrypt.Net.BCrypt.Verify(loginVm.Password, storedUser.Password);
        }

    }
}