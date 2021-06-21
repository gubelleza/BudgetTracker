using System.Collections.Generic;
using System.Linq;
using BudgetTracker.Models.Enums;
using BudgetTracker.Models.Transactions;
using BudgetTracker.Services.Interfaces;

namespace BudgetTracker.Services
{
    public class StatsService : IStatsService
    {
        public decimal CalcMean(List<Transaction> transactions, TransactionType transactionType)
        {
            return transactions
                .Where(t => t.TransactionCategory.TransactionType == transactionType)
                .Select(t => t.Amount)
                .Average();

        }

        public Transaction GetMin(List<Transaction> transactions, TransactionType transactionType)
        {
            return transactions
                .OrderBy(t => t.Amount)
                .FirstOrDefault(t => t.TransactionCategory.TransactionType == transactionType);
        }

        public Transaction GetMax(List<Transaction> transactions, TransactionType transactionType)
        {
            return transactions
                .OrderByDescending(t => t.Amount)
                .FirstOrDefault(t => t.TransactionCategory.TransactionType == transactionType);
        }

        public decimal CalcDifference(List<Transaction> transactions, string username)
        {
            var totalIncomes = transactions
                .Where(t => t.BudgetMemberName == username && 
                            t.TransactionCategory.TransactionType == TransactionType.Income)
                .Select(t => t.Amount).Sum();
            
            var totalExpenses = transactions
                .Where(t => t.BudgetMemberName == username && 
                            t.TransactionCategory.TransactionType == TransactionType.Expense)
                .Select(t => t.Amount).Sum();

            return totalIncomes - totalExpenses;
        }
    }
}