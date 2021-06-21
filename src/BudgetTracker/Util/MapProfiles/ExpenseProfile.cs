using AutoMapper;
using BudgetTracker.Models.Transactions;
using BudgetTracker.Models.ViewModels;

namespace BudgetTracker.Util.MapProfiles
{
    public class ExpenseProfile : Profile
    {
        public ExpenseProfile()
        {
            CreateMap<TransactionsTableViewModel, Transaction>()
                .ForMember(
                    e => e.TransactionDate, 
                    o => o.MapFrom((src, dest, tp) => src.TransactionDate ?? dest.TransactionDate))
                .ForAllMembers(o => o.Condition((src, dest, sourceMember) => sourceMember != null));

            CreateMap<AddTransactionViewModel, Transaction>();
        }
    }
}