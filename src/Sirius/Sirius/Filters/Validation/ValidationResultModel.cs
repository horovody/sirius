using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Sirius.Filters.Validation
{
    public class ValidationResultModel
    {
        public string Message { get; }

        public List<ValidationError> Errors { get; }

        public ValidationResultModel(ModelStateDictionary modelState)
        {
            this.Message = "В ходе проверки найдены ошибки.";
            this.Errors = modelState.Keys
                .SelectMany(key => modelState[key].Errors.Select(x =>
                {
                    var errorMessage = x.ErrorMessage;
                    if (string.IsNullOrEmpty(errorMessage) && x.Exception != null)
                    {
                        errorMessage = "Некорректное значение поля";
                    }
                    return new ValidationError(key, errorMessage);
                }))
                .ToList();
        }
    }
}
