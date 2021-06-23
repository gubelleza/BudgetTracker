using BudgetTracker.Data;
using BudgetTracker.Models.Users;
using EntityFrameworkCoreMock;
using BudgetTracker.UnitTests.Mocks.Configurations;
using Moq;
using static BudgetTracker.UnitTests.Mocks.Configurations.DbContextConfigurationFactory;

namespace BudgetTracker.UnitTests.Mocks
{
    public class ApplicationDbContextMock : DbContextMock<ApplicationDbContext>
    {
        public DbSetMock<User> Users { get; set; }

        public ApplicationDbContextMock(DbContextConfiguration configuration)
        {
            Users = CreateDbSetMock(set => set.Users, configuration.InitialUsers);
        }

        public static ApplicationDbContextMock PopulatedDbContextFactory =>
            new ApplicationDbContextMock(PopulatedDbContextConfiguration);
        
        public static ApplicationDbContextMock EmptyDbContextFactory => 
            new ApplicationDbContextMock(EmptyDbContextConfiguration);

        public ApplicationDbContextMock SetupAddUser(User user)
        {
            Users.Setup(db => db.Add(It.IsAny<User>()))
                .Verifiable();
            return this;
        }

        public ApplicationDbContextMock VerifyAddAnUser(User user, Times times)
        {
            Users.Verify(db => db.Add(It.Is<User>(
                u => u.Username == user.Username &&
                     u.Email == user.Email)), times);
            return this;
        }
    }
}