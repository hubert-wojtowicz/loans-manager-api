using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace LoansManager.Controllers
{
    public class LoansBaseController : ControllerBase
    {
        protected ValidationResult ValidationResultFactory(string propertyName, object attemptedValue, string errorMessageTemplate, params string[] templateParams)
        {
            var failure = new ValidationFailure(propertyName, string.Format(errorMessageTemplate, templateParams));
            failure.AttemptedValue = attemptedValue;
            return new ValidationResult(new[] { failure });
        }
    }
}
