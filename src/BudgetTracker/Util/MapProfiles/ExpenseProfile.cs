using AutoMapper;
using BudgetTracker.Models.Expenses;
using BudgetTracker.Models.ViewModels;

namespace BudgetTracker.Util.MapProfiles
{
    public class ExpenseProfile : Profile
    {
        public ExpenseProfile()
        {
            CreateMap<ExpenseEditViewModel, Expense>()
                .ForMember(e => e.PaidAt, o => o.Condition(vm => vm.PaidAt != default))
                .ForAllMembers(o => o.Condition((src, dest, sourceMember) => sourceMember != null));

            CreateMap<CreateExpenseViewModel, Expense>();
        }
    }
}