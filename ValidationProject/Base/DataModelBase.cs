using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ValidationProject.Base
{
    using ValidationRules;
    using Extensions;
    using System.ComponentModel.DataAnnotations;

    public class DataModelBase
    {
        private const string GeneralRule = "General";

        public DataModelBase()
        {
            AddValidationRules();
        }

        private IDictionary<string, ICollection<IValidationCommand>> _validationRules = new Dictionary<string, ICollection<IValidationCommand>>();

        public void AddValidationRule(IValidationCommand rule)
        {
            AddValidationRule(GeneralRule, rule);
        }

        public void AddValidationRule<T>(Expression<Func<T>> property, IValidationCommand rule)
        {
            string propertyName = property.GetPropertyName();
            AddValidationRule(propertyName, rule);
        }

        private void AddValidationRule(string propertyName, IValidationCommand rule)
        {
            if (!_validationRules.ContainsKey(propertyName))
            {
                _validationRules.Add(propertyName, new List<IValidationCommand>());
            }
            if (!_validationRules[propertyName].Contains(rule))
                _validationRules[propertyName].Add(rule);
        }

        protected virtual void AddValidationRules()
        {
            //todo: Add validation rule for data annotations
        }


        private IDictionary<string, ICollection<ValidationResult>> _errors = new Dictionary<string, ICollection<ValidationResult>>();

        public virtual bool Validate(string propertyName)
        {
            var validationCommands = GetValidationRules(propertyName);

            var validationContext = new ValidationCommandContext();

            validationCommands.ForEach(c => c.ExecuteCommand(validationContext));

            ClearErrors(propertyName);

            if (!validationContext.IsValid)
            {
                AddErrors(propertyName, validationContext.Errors);
            }

            return validationContext.IsValid;
        }

        public virtual bool Validate()
        {
            ClearErrors();
            var validationContext = new ValidationCommandContext();
            _validationRules.Values.SelectMany(c => c).ForEach(c => c.ExecuteCommand(validationContext));

           if (!validationContext.IsValid)
            {
                foreach (var validationResult in validationContext.Errors)
                {
                    var propertyNames = validationResult.MemberNames.Any() ? validationResult.MemberNames : new string[] { "" };
                    foreach (string propertyName in propertyNames)
                    {
                        if (!_errors.ContainsKey(propertyName))
                        {
                            _errors.Add(propertyName, new List<ValidationResult>() { validationResult });
                        }
                        else
                        {
                            _errors[propertyName].Add(validationResult);
                        }
                        RaiseErrorsChanged(propertyName);
                    }
                }
                return false;
            }
            return true;
        }

        private IEnumerable<IValidationCommand> GetValidationRules(string propertyName)
        {
            if (!_validationRules.ContainsKey(propertyName))
            {
                _validationRules.Add(propertyName, new List<IValidationCommand>());
            }
            return _validationRules[propertyName];
        }

        public void ClearErrors()
        {
            var list = _errors.Keys.ToList();
            _errors.Clear();
            list.ForEach(RaiseErrorsChanged);
        }

        private void ClearErrors(string propertyName)
        {
            if (_errors.ContainsKey(propertyName))
            {
                _errors.Remove(propertyName);
                RaiseErrorsChanged(propertyName);
            }
        }

        private void AddErrors(string propertyName, ICollection<ValidationResult> errors)
        {
            if (!string.IsNullOrEmpty(propertyName))
            {
                if (!_errors.ContainsKey(propertyName))
                {
                    _errors.Add(propertyName, errors);
                }
                else
                {
                    errors.ForEach(e => _errors[propertyName].Add(e));
                }
                RaiseErrorsChanged(propertyName);
            }
        }

        public void RaiseErrorsChanged(string propertyName = "")
        { }
    }
}
