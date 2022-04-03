using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Portfolio.Shared.Extensions
{
    public static class FluenValidationError
    {
        public static List<string> FluentValidationErrorToListString(this List<ValidationFailure> validationFailures)
        {
            List<string> errors = new List<string>();
            foreach (var item in validationFailures)
            {
                errors.Add(item.ErrorMessage);
            }
            return errors;
        }
    }
}
