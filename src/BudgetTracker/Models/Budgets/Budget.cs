using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BudgetTracker.Models.Transactions;

namespace BudgetTracker.Models.Budgets
{
    public class Budget
    {
        [Key]
        public Guid BudgetId { get; set; }
        
        public string Name { get; set; }
                
        [NotMapped]
        public virtual List<Transaction> Transactions { get; set; }

        [NotMapped]
        public virtual List<TransactionCategory> TransactionsCategories { get; set; }

        [NotMapped]
        public virtual List<BudgetMember> BudgetMembers { get; set; }
    }
}