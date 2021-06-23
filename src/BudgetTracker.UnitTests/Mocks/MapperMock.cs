using AutoMapper;
using BudgetTracker.Models.Users;
using BudgetTracker.Models.ViewModels;
using BudgetTracker.Util.MapProfiles;
using Moq;

namespace BudgetTracker.UnitTests.Mocks
{
    public class MapperMock : Mock<IMapper>
    {
        private IMapper _mapper;
        
        public MapperMock(Profile mapperProfile)
        {
            _mapper = new Mapper(new MapperConfiguration(c => c.AddProfile(mapperProfile)));
        }

        public static MapperMock UserMapperMockFactory => new MapperMock(new UserProfile());
        
        public MapperMock SetupCreateUserViewModelToUser(CreateUserViewModel createUserVm)
        {
            Setup(map => map.Map<User>(It.Is<CreateUserViewModel>(
                    vm => vm.Username == createUserVm.Username &&
                          vm.Password == createUserVm.Password &&
                          vm.ConfirmPassword == createUserVm.ConfirmPassword &&
                          vm.Email == createUserVm.Email &&
                          vm.ConfirmEmail == createUserVm.ConfirmEmail)))
                .Returns(_mapper.Map<User>(createUserVm))
                .Verifiable();
            return this;
        }
    }
}