using System;
using System.Linq;
using BudgetTracker.Models.Users;
using BudgetTracker.Models.ViewModels;
using BudgetTracker.Services;
using BudgetTracker.UnitTests.Mocks;
using BudgetTracker.UnitTests.TestData.Users;
using BudgetTracker.UnitTests.TestUtil;
using BudgetTracker.Util.MapProfiles;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Moq;
using Xunit;

namespace BudgetTracker.UnitTests.Tests.Services
{
    public class AuthenticationServiceTest
    {
         [Theory]
         [InlineData(DbSetUsers.UsersIds.GUSTAVO)]
         [InlineData(DbSetUsers.UsersIds.JOYCE)]
         [InlineData(DbSetUsers.UsersIds.MARVIN)]
         public void Login_ModelStateIsValid_UserAuthenticated(string userId)
         {
             // Arrange
             // test data
             var testUser = DbSetUsers.TestUsers.Single(u => u.UserId == userId.ToGuid());
             var loginVm = UserToLoginViewModel(testUser, useEmailToLogin: false);
             
             // mocks
             var dbContextMock = ApplicationDbContextMock.PopulatedDbContextFactory;
             var sessionMock = SessionMock.DefaultSessionMockFactory.SetupSet();
             
             // Act
             var result = new AuthenticationService(dbContextMock.Object)
                 .Login(loginVm, new ModelStateDictionary(), sessionMock.Object);
             
             // Assert
             result.Should().Be(true);
             sessionMock.VerifySetUserId(testUser).VerifySetUserName(testUser);
         }
         
         private LoginViewModel UserToLoginViewModel(User user, bool useEmailToLogin)
         {
             return new LoginViewModel
             {
                 UsernameOrEmail = useEmailToLogin? user.Email : user.Username,
                 Password = user.Password
             };
         }
    }
}