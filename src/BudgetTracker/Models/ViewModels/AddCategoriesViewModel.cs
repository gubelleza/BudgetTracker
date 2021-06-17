using System;
using System.Collections.Generic;
using BudgetTracker.Models.Expenses;

namespace BudgetTracker.Models.ViewModels
{
    public class AddCategoriesViewModel<TCategory>
    {
        public List<TCategory> Categories { get; set; }
        public Guid BudgetId { get; set; }
    }
}