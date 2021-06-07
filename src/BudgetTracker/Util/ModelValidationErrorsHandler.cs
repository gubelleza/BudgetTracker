using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BudgetTracker.Util
{
    public class ModelValidationErrorsHandler
    {
        public Dictionary<string, string> ModelErrors { get; private set; }
        
        public ModelValidationErrorsHandler()
        {
            ModelErrors = new Dictionary<string, string>();
        }
        
        public void ExtractValidationErrors<TEntity>(ModelStateDictionary modelState)
        {
            var propertiesNames = 
                typeof(TEntity)
                .GetProperties()
                .Select(p => p.Name)
                .ToList();
            
            propertiesNames.ForEach(n =>
            {
                if (modelState.TryGetValue(n, out var error) && error.ValidationState == ModelValidationState.Invalid)
                    ModelErrors.Add(n, error.Errors.Select(c => c.ErrorMessage).FirstOrDefault());
            });
        }
    }
}