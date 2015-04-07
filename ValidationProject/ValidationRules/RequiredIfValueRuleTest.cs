using NUnit.Framework;

namespace ValidationProject.ValidationRules
{
    [TestFixture]
    public class RequiredIfValueRuleTest : ValidationRuleTestBase
    {
        private sealed class TestObject
        {
            public object Weight { get; set; }
            public int? NullableType { get; set; }
        }

        [Test]
        [TestCase(true, 12.43, true)]
        [TestCase(true, 12.43, false)]
        [TestCase(false, null, true)]
        [TestCase(true, null, false)]
        public void RequiredIfValueTest(bool expected, object value, bool condition)
        {
            var rule = Arrange<RequiredIfValue, TestObject>((RequiredIfValue, testObject) =>
            {
                testObject.Weight = value;
                RequiredIfValue.Condition = () => condition;
                RequiredIfValue.RequiredProperty = () => testObject.Weight;
            });

            Act(rule);

            Assert.That(_context.IsValid == expected);
        }

        [Test]
        public void  RuleShouldAcceptNullableTypesTest()
        {
            var rule = Arrange<RequiredIfValue, TestObject>((RequiredIfValue, testObject) =>
            {
                testObject.NullableType = 3;
                RequiredIfValue.Condition = () => true;
                RequiredIfValue.RequiredProperty = () => testObject.NullableType;
            });

            Act(rule);

            Assert.That(_context.IsValid);
        }
    }
}