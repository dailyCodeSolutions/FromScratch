using NUnit.Framework;

namespace ValidationProject.ValidationRules
{
    [TestFixture]
    public class RequiredRuleTest : ValidationRuleTestBase
    {
        [Test]
        [TestCase(null, false)]
        [TestCase("", false)]
        [TestCase("  ", false)]
        [TestCase(23, true)]
        [TestCase("string value", true)]
        public void TestRule(object value, bool expected)
        {
            var rule = Arrange<RequiredRule, object>((ruleRequired, testObject) =>
            {
                testObject = value;
                ruleRequired.PropertyRequired = () => testObject;
            });
           
            Act(rule);
            
            Assert.That(_context.IsValid == expected);
        }
    }
}