using System.Collections.Generic;
using BudgetTracker.Models.Expenses;

namespace BudgetTracker.Models.ViewModels
{
    public class AddCategoriesViewModel
    {
        public List<ExpenseCategory> Categories { get; set; }
    }
}