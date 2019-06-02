using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace LoansManager.Controllers
{
    public class ApplicationBaseController : ControllerBase
    {
        protected static ValidationResult ValidationResultFactory(string propertyName, object attemptedValue, string errorMessageTemplate, params string[] templateParams)
        {
            var failure = new ValidationFailure(propertyName, string.Format(errorMessageTemplate, templateParams))
            {
                AttemptedValue = attemptedValue,
            };
            return new ValidationResult(new[] { failure });
        }
    }
}