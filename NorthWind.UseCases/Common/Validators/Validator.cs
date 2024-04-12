using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace NorthWind.UseCases.Common.Validators
{
    public static class Validator
    {
        public static Task<List<ValidationFailure>>Validate(Model model, IEnumerable<IValidator<Model>> validators, bool causesException = true)
        {
            var Failures = validators
               .Select(v => v.Validate(model))
               .SelectMany(r => r.Errors)
               .Where(f => f != null)
               .ToList();
            if (Failures.Any() && causesException)
            {
                throw new ValidationException(Failures);
            }
            return Task.FromResult(Failures);
        }
    }
}
