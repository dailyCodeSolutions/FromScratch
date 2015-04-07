using System;
using System.Linq.Expressions;

namespace ValidationProject.Extensions
{
    public static class ReflectionExtensions
    {
        public static object GetPropertyValue(this object obj, string propertyName)
        {
            var type = obj.GetType();
            var propertyInfo = type.GetProperty(propertyName);

            if (propertyInfo == null)
                return null;

            return propertyInfo.GetValue(obj);
        }

        public static string GetPropertyName<T>(this Expression<Func<T>> property)
        {
            var memberExpression = property.Body as MemberExpression;
            if (property.Body is UnaryExpression)
            {
                var unaryExpression = property.Body as UnaryExpression;
                memberExpression = unaryExpression.Operand as MemberExpression;
            }
            
            if (memberExpression != null)
            {
                return memberExpression.Member.Name;
            }
            return null;
        }
    }
}
