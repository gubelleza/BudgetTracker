using BudgetTracker.Models.Enums;
using BudgetTracker.Models.Transactions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetTracker.Models.ViewModels
{
    public class AddTransactionViewModel 
    {
        [Required]
        public Recurrence Recurrence { get; set; }
        
        [Required]
        public decimal? Amount { get; set; }
        
        [Required]
        public DateTime? TransactionDate { get; set; }
        
        public Guid BudgetId { get; set; }
        
        public string BudgetMemberName { get; set; }
        
        public int? TransactionCategoryId { get; set; }
        
        public List<TransactionCategory> CurrentCategories { get; set; }
        public List<string> BudgetMembersNames { get; set; }
    }
}
