using AutoMapper;
using BudgetTracker.Models.Users;
using BudgetTracker.Models.ViewModels;

namespace BudgetTracker.Util.MapProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<CreateUserViewModel, User>()
                .ForMember(u => u.Password, 
                    options => options.MapFrom(vm => BCrypt.Net.BCrypt.HashPassword(vm.Password)));
        }
    }
}