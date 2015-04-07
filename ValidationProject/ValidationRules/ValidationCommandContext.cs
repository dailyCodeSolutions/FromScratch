using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ValidationProject.ValidationRules
{
    public class ValidationCommandContext
    {
        ICollection<ValidationResult> _errors = new List<ValidationResult>();
        public bool IsValid { get { return !_errors.Any(); }}
        public ICollection<ValidationResult> Errors { get { return _errors; }  }

        public void AddError(ValidationResult validationResult)
        {
            _errors.Add(validationResult);
        }
    }
}