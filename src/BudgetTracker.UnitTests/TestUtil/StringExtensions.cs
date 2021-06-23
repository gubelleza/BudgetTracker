using System;

namespace BudgetTracker.UnitTests.TestUtil
{
    public static class StringExtensions
    {
        public static Guid ToGuid(this string str)
        {
            return new Guid(str);
        }
    }
}