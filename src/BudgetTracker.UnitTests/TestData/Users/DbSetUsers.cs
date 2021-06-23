using System;
using System.Collections.Generic;
using BudgetTracker.Models.Users;
using BudgetTracker.UnitTests.TestUtil;

namespace BudgetTracker.UnitTests.TestData.Users
{
    public static class DbSetUsers
    {
        public static class UsersIds
        {
            public const string GUSTAVO = "1FBD7B24-2686-4231-A7F4-86B5FB0D62DF";
            public const string JOYCE = "C2CACFD2-A95D-4EBD-AD8C-EFE8BA7CC86A";
            public const string MARVIN = "7CE507F9-E039-43A9-A1B5-39AB6F639FE2";
        }

        public static IEnumerable<User> TestUsers => new[]
        {
            new User
            {
                UserId = UsersIds.GUSTAVO.ToGuid(),
                Email = "gustavo.test@email.com",
                Username = "gustavo",
                Password = "test"
            },
            new User
            {
                UserId = UsersIds.JOYCE.ToGuid(),
                Email = "joyce.test@email.com",
                Username = "joyce",
                Password = "test"
            },
            new User
            {
                UserId = UsersIds.MARVIN.ToGuid(),
                Email = "marvin.test@email.com",
                Username = "marvin",
                Password = "test"
            }
        };
    }
}