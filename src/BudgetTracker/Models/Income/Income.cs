using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BudgetTracker.Models.Enums;
using BudgetTracker.Models.Users;

namespace BudgetTracker.Models.Income
{
    public class Income
    {
        [Key]
        public int IncomeId { get; set; }
        
        public Recurrence Recurrence { get; set; }
        public decimal AmountReceived { get; set; }
        public DateTime ReceivedAt { get; set; }
        public string BudgetMemberName { get; set; }
        
        [ForeignKey("IncomeCategory")]
        public int IncomeCategoryId { get; set; }
        public IncomeCategory IncomeCategory { get; set; }
        
        [ForeignKey("User")]
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}