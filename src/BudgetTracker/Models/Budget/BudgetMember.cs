using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BudgetTracker.Models.Users;

namespace BudgetTracker.Models.Budget
{
    public class BudgetMember
    {
        [Key]
        public Guid BudgetMemberId { get; set; }
        
        [ForeignKey("Budget")]
        public Guid BudgetId { get; set; }
        public Budget Budget { get; set; }
        
        [ForeignKey("User")]
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}