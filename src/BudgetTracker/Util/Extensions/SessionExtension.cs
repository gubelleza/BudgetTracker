using System;
using Microsoft.AspNetCore.Http;

namespace BudgetTracker.Util.Extensions
{
    public static class SessionExtensions
    {
        public static void SetUserId(this ISession session, Guid id)
        {
            session.SetString("UserId", id.ToString());
        }

        public static bool TryGetUserId(this ISession session, out Guid? idOutput)
        {
            idOutput = GetUserId(session);
            return idOutput != null;
        }

        public static Guid? GetUserId(this ISession session)
        {
            var userId = session.GetString("UserId");
            return userId == null ? null : new Guid(userId);
        }

        public static void SetUsername(this ISession session, string username)
        {
            session.SetString("Username", username);
        }

        public static string GetUsername(this ISession session)
        {
            return session.GetString("Username");
        }
        
        public static void RemoveUserData(this ISession session)
        {
            session.Remove("UserId");
            session.Remove("Username");
        }

        public static void SetCurrentBudgetId(this ISession session, Guid budgetId)
        {
            session.Remove("BudgetId");
            session.SetString("BudgetId", budgetId.ToString());
        }

        public static bool TryGetBudgetId(this ISession session, out Guid? idOutput)
        {
            idOutput = GetCurrentBudgetId(session);
            return idOutput != null;
        }
        
        public static Guid GetCurrentBudgetId(this ISession session)
        {
            return new Guid(session.GetString("BudgetId"));
        }
    }
}