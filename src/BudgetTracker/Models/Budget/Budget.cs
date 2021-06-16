using System;
using System.ComponentModel.DataAnnotations;

namespace BudgetTracker.Models.Budget
{
    public class Budget
    {
        [Key]
        public Guid BudgetId { get; set; }
        
        public string Name { get; set; }
        
        
    }
}