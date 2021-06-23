using System;
using System.Collections.Generic;
using System.Linq;
using BudgetTracker.Models.Users;
using BudgetTracker.UnitTests.TestData.Users;

namespace BudgetTracker.UnitTests.Mocks.Configurations
{
    public static class DbContextConfigurationFactory
    {
        public static DbContextConfiguration PopulatedDbContextConfiguration => new DbContextConfiguration
        {
            InitialUsers = DbSetUsers.TestUsers
        };

        public static DbContextConfiguration EmptyDbContextConfiguration => new DbContextConfiguration();
    }
    
    public class DbContextConfiguration
    {
        private IEnumerable<User> _initialUsers;
        public IEnumerable<User> InitialUsers
        {
            get => _initialUsers ?? Array.Empty<User>();
            set => _initialUsers = UsersWithHashedPasswords(value);
        }

        private IEnumerable<User> UsersWithHashedPasswords(IEnumerable<User> users)
        {
            return users.Select(u =>
            {
                u.Password = BCrypt.Net.BCrypt.HashPassword(u.Password);
                return u;
            });
        }
    }
}