using System;
using System.Collections.Generic;
using BudgetTracker.Models.Transactions;

namespace BudgetTracker.Models.ViewModels
{
    public class AddCategoriesViewModel
    {
        public List<TransactionCategory> Categories { get; set; }
        public Guid BudgetId { get; set; }
    }
}