using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace src.Helpers
{
    public static class ModelStateHelper
    {
        public static List<string> GetErrorMessages(this ModelStateDictionary dictionary)
        {
            return dictionary.SelectMany(m => m.Value.Errors)
                            .Select(m => m.ErrorMessage)
                            .ToList();
        }
    }
}