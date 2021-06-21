using System.Collections.Generic;
using BudgetTracker.Models.Enums;
using BudgetTracker.Models.Transactions;

namespace BudgetTracker.Services.Interfaces
{
    public interface IStatsService
    {
        public decimal CalcMean(List<Transaction> transactions, TransactionType transactionType);
        public Transaction GetMin(List<Transaction> transactions, TransactionType transactionType);
        public Transaction GetMax(List<Transaction> transactions, TransactionType transactionType);
        public decimal CalcDifference(List<Transaction> transactions, string username);
    }
}