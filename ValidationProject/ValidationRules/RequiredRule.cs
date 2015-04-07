using System;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace ValidationProject.ValidationRules
{
    using Extensions;
    public class RequiredRule : IValidationCommand
    {
        public void ExecuteCommand(ValidationCommandContext context)
        {
            object value = PropertyRequired.Compile().Invoke();
            var propertyName = PropertyRequired.GetPropertyName();

            bool result = value == null;
                
            if (value is string)
                result = string.IsNullOrWhiteSpace(value as string);

            if (result)
            {
                context.AddError(new ValidationResult(ErrorMessage, new[] { propertyName }));
            }
        }
        
        public string ErrorMessage { get; set; }
        public Expression<Func<object>> PropertyRequired { get; set; }
    }
}