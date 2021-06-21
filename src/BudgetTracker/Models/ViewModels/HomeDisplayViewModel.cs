using Microsoft.AspNetCore.Mvc;
using System;

namespace BudgetTracker.Models.ViewModels
{
    public class HomeDisplayViewModel
    {
        public Guid BudgetId { get; set; }

        [Remote(action: "CheckUsernameExists", controller: "User", ErrorMessage = "User don't exist")]
        public string Username { get; set; }
        public TransactionsTableViewModel ExpenseTableViewModel { get; set; }
    }
}