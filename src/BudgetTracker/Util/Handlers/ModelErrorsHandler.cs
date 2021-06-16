using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Linq;

namespace BudgetTracker.Util.Handlers
{
    public static class ModelErrorsHandler
    {
        public static Dictionary<string, string> ModelStateToErrorDict(ModelStateDictionary modelState)
        {
            var errorDict = new Dictionary<string, string>();

            foreach(var errorKey in modelState.Keys)
            {
                if (modelState.TryGetValue(errorKey, out var stateEntry) && 
                    stateEntry.ValidationState == ModelValidationState.Invalid)
                {
                    errorDict.Add(
                        errorKey, 
                        stateEntry
                            .Errors.Select(e => e.ErrorMessage).FirstOrDefault());
                }
            }
            return errorDict;
        }

        public static void PopulateModelState(
            ModelStateDictionary modelState, Dictionary<string, string> errorsDict)
        {
                foreach(var (key, value) in errorsDict)
                    modelState.AddModelError(key, value);
        }
    }
}