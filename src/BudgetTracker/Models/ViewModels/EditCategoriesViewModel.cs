using System;
using System.Collections.Generic;
using BudgetTracker.Models.Expenses;

namespace BudgetTracker.Models.ViewModels
{
    public class EditCategoriesViewModel
    {
        public List<ExpenseCategory> Categories { get; set; }
        public List<CategoryToDelete> DeleteCategories { get; set; }

        public void PopulateCategoriesToDelete(int quantity)
        {
            DeleteCategories = new List<CategoryToDelete>();
            for(var i = 0; i < quantity; i++)
                DeleteCategories.Add(new CategoryToDelete());
                
        }
    }

    public class CategoryToDelete
    {
        public int Id { get; set; }
        public bool Deleted { get; set; }
    }
}