using BudgetTracker.Models.Users;
using Microsoft.AspNetCore.Http;
using Moq;
using System.Text;

namespace BudgetTracker.UnitTests.Mocks
{
    public class SessionMock : Mock<ISession>
    {
        public static SessionMock DefaultSessionMockFactory => new SessionMock();
        
        public SessionMock SetupSet()
        {
            Setup(s => s.Set(It.IsAny<string>(), It.IsAny<byte[]>()))
                .Verifiable();

            return this;
        }
        
        public SessionMock VerifySetUserId(User user)
        {
            Verify(s => s.Set(
                    It.Is<string>(x => x == "UserId"), 
                    It.Is<byte[]>(x => Encoding.UTF8.GetString(x) == user.UserId.ToString())));

            return this;
        }
        
        public SessionMock VerifySetUserName(User user)
        {
            Setup(s => s.Set(
                    It.Is<string>(x => x == "Username"), 
                    It.Is<byte[]>(x => Encoding.UTF8.GetString(x) == user.Username)));

            return this;
        }
    }
}