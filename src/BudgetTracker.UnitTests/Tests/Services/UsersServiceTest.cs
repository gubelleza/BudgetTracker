using System;
using System.Linq;
using BudgetTracker.Models.Users;
using BudgetTracker.Models.ViewModels;
using BudgetTracker.Services;
using FluentAssertions;
using BudgetTracker.UnitTests.Mocks;
using BudgetTracker.UnitTests.TestData.Users;
using BudgetTracker.UnitTests.TestUtil;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Moq;
using Xunit;
using static BudgetTracker.Util.Constants.ModelStateErrors;

namespace BudgetTracker.UnitTests.Tests.Services
{
    public class UsersServiceTest
    {
        [Theory]
        [InlineData(DbSetUsers.UsersIds.GUSTAVO)]
        [InlineData(DbSetUsers.UsersIds.JOYCE)]
        [InlineData(DbSetUsers.UsersIds.MARVIN)]
        public void CreateUser_ModelStateIsValid_UserCreated(string userId)
        {
            // Arrange
            // test data
            var testUser = DbSetUsers.TestUsers.Single(u => u.UserId == userId.ToGuid());
            var testCreateUserVm = UserToCreateUserViewModel(testUser);
            
            // mocks
            var dbContextMock = ApplicationDbContextMock.EmptyDbContextFactory.SetupAddUser(testUser);
            var mapperMock = MapperMock.UserMapperMockFactory.SetupCreateUserViewModelToUser(testCreateUserVm);
            
            // Act
            var result = new UsersService(dbContextMock.Object, mapperMock.Object)
                .CreateUser(testCreateUserVm, new ModelStateDictionary());
            
            // Assert
            result.Should().Be(true);
            dbContextMock.VerifyAddAnUser(testUser, Times.Once());
        }
        
        [Theory]
        [InlineData(DbSetUsers.UsersIds.GUSTAVO)]
        [InlineData(DbSetUsers.UsersIds.JOYCE)]
        [InlineData(DbSetUsers.UsersIds.MARVIN)]
        public void CreateUser_UsernameAndEmailAlreadyExist_UserNotCreated(string userId)
        {
            // Arrange
            // test data
            var testUser = DbSetUsers.TestUsers.Single(u => u.UserId == userId.ToGuid());
            var testCreateUserVm = UserToCreateUserViewModel(testUser);
            
            // model state
            var modelState = new ModelStateDictionary();
            
            // mocks
            var dbContextMock = ApplicationDbContextMock.PopulatedDbContextFactory.SetupAddUser(testUser);
            var mapperMock = MapperMock.UserMapperMockFactory.SetupCreateUserViewModelToUser(testCreateUserVm);
            
            // Act
            var result = new UsersService(dbContextMock.Object, mapperMock.Object)
                .CreateUser(testCreateUserVm, modelState);
            
            // Assert
            result.Should().Be(false);
            dbContextMock.VerifyAddAnUser(testUser, Times.Never());
            modelState.Should().ContainKey(nameof(testCreateUserVm.Username))
                .WhoseValue.Errors.Should().Satisfy(e => e.ErrorMessage == DATA_ALREADY_EXISTS);
            modelState.Should().ContainKey(nameof(testCreateUserVm.Email))
                .WhoseValue.Errors.Should().Satisfy(e => e.ErrorMessage == DATA_ALREADY_EXISTS);;
        }
        
        [Theory]
        [InlineData(DbSetUsers.UsersIds.GUSTAVO)]
        [InlineData(DbSetUsers.UsersIds.JOYCE)]
        [InlineData(DbSetUsers.UsersIds.MARVIN)]
        public void CreateUser_PasswordsAndEmailDontMatch_UserNotCreated(string userId)
        {
            // Arrange
            // test data
            var testUser = DbSetUsers.TestUsers.Single(u => u.UserId == userId.ToGuid());
            var testCreateUserVm = UserToCreateUserViewModel(testUser, changeProperties: vm =>
                {
                    vm.ConfirmPassword += "err_";
                    vm.ConfirmEmail += "err_";
                });
            
            // model state
            var modelState = new ModelStateDictionary();
            
            // mocks
            var dbContextMock = ApplicationDbContextMock.EmptyDbContextFactory.SetupAddUser(testUser);
            var mapperMock = MapperMock.UserMapperMockFactory.SetupCreateUserViewModelToUser(testCreateUserVm);
            
            // Act
            var result = new UsersService(dbContextMock.Object, mapperMock.Object)
                .CreateUser(testCreateUserVm, modelState);
            
            // Assert
            result.Should().Be(false);
            dbContextMock.VerifyAddAnUser(testUser, Times.Never());
            modelState.Should().ContainKey(nameof(testCreateUserVm.ConfirmPassword))
                .WhoseValue.Errors.Should().Satisfy(e => e.ErrorMessage == PASSWORDS_MUST_MATCH);
            modelState.Should().ContainKey(nameof(testCreateUserVm.ConfirmEmail))
                .WhoseValue.Errors.Should().Satisfy(e => e.ErrorMessage == EMAILS_MUST_MATCH);;
        }
        
        private CreateUserViewModel UserToCreateUserViewModel(
            User user, Action<CreateUserViewModel> changeProperties = null)
        {
            var createUserVm = new CreateUserViewModel
            {
                Username = user.Username,
                Email = user.Email,
                ConfirmEmail = user.Email,
                Password = user.Password,
                ConfirmPassword = user.Password
            };
            changeProperties?.Invoke(createUserVm);
            return createUserVm;
        }
    }
}