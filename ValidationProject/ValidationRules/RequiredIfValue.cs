using System;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace ValidationProject.ValidationRules
{
    using Extensions;
    public class RequiredIfValue : IValidationCommand
    {
        public void ExecuteCommand(ValidationCommandContext context)
        {
            bool condition = Condition.Invoke();
            object value = RequiredProperty.Compile().Invoke();
            var propertyName = RequiredProperty.GetPropertyName();

            if (value == null && condition)
            {
                context.AddError(new ValidationResult(ErrorMessage, new[] {propertyName}));
            }
        }

        public string ErrorMessage { get; set; }
        public Expression<Func<object>> RequiredProperty { get; set; }
        public Func<bool> Condition { get; set; }
    }
}