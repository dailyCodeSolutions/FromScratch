using System;

namespace ValidationProject.ValidationRules
{
    public class ValidationRuleTestBase
    {
        protected ValidationCommandContext _context;

        protected void Act(IValidationCommand rule)
        {
            _context = new ValidationCommandContext();
            rule.ExecuteCommand(_context);
        }

        protected T Arrange<T,U>(Action<T, U> ruleInit) where T : IValidationCommand, new() where U : new()
        {
            T rule = new T();
            U _testObject = new U();
            ruleInit(rule, _testObject);
            return rule;
        }

       
    }
}