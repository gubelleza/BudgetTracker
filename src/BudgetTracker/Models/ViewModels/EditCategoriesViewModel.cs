using System;
using System.Collections.Generic;
using BudgetTracker.Models.Transactions;

namespace BudgetTracker.Models.ViewModels
{
    public class EditCategoriesViewModel
    {
        public List<TransactionCategory> Categories { get; set; }
        public List<CategoryToDelete> DeleteCategories { get; set; }

        public Guid BudgetId { get; set; }
        
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