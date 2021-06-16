using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BudgetTracker.Models.Budgets;

namespace BudgetTracker.Models.ViewModels
{
    public class UserBudgetsViewModel
    {
        [MinLength(3)]
        public string BudgetName { get; set; }
        public List<Budget> Budgets { get; set; } 
    }
}