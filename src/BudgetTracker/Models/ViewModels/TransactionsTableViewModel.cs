using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using BudgetTracker.Models.Enums;
using BudgetTracker.Models.Transactions;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BudgetTracker.Models.ViewModels
{
    public class TransactionsTableViewModel
    {
        #region DisplayExpenses
        [BindNever]
        public List<IGrouping<string, Transaction>> Transactions { get; set; }
        
        [BindNever]
        public bool HasTransactions => Transactions != null && Transactions.Any();
        #endregion
        
        #region ExpenseInput
        public int TransctionId { get; set; }
        public Recurrence Recurrence { get; set; }
        
        [Range(1.0, double.MaxValue, ErrorMessage = "Amount paid be zero")]
        public decimal? Amount { get; set; }
        
        public DateTime? TransactionDate { get; set; }
        public string BudgetMemberName { get; set; }
        public int? TransactionCategoryId { get; set; }
        
        public Guid BudgetId { get; set; }
        #endregion
        
        #region ExpenseFormAttributes
        [BindNever]
        public List<TransactionCategory> CurrentCategories { get; set; }
        
        [BindNever]
        public List<string> BudgetMembersNames { get; set; }
        #endregion
    }
}