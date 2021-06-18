using System;
using System.Collections.Generic;
using System.Linq;
using BudgetTracker.Models.Expenses;
using Microsoft.AspNetCore.Mvc;

namespace BudgetTracker.Models.ViewModels
{
    public class HomeDisplayViewModel
    {
        public Guid BudgetId { get; set; }
        
        [Remote(action:"CheckUsernameExists", controller:"User", ErrorMessage = "User don't exist")]
        public string Username { get; set; }
        public ExpenseTableViewModel ExpenseTableViewModel { get; set; }
    }
}