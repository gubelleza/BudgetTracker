using AutoMapper;
using BudgetTracker.Models.Expenses;
using BudgetTracker.Models.ViewModels;

namespace BudgetTracker.Util.MapProfiles
{
    public class ExpenseProfile : Profile
    {
        public ExpenseProfile()
        {
            CreateMap<ExpenseTableViewModel, Expense>()
                .ForMember(
                    e => e.PaidAt, 
                    o => o.MapFrom((src, dest, tp) => src.PaidAt ?? dest.PaidAt))
                .ForAllMembers(o => o.Condition((src, dest, sourceMember) => sourceMember != null));

            CreateMap<CreateExpenseViewModel, Expense>();
        }
    }
}