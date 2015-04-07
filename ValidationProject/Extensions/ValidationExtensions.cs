using System;
using System.Collections.Generic;
using System.Text;

namespace ValidationProject.Extensions
{
    public static class ValidationExtensions
    {
        public static string ToErrorString<T>(this ICollection<T> collection, Func<T, string> valueFunc)
        {
            var builder = new StringBuilder();
            collection.ForEach(i => builder.AppendLine(valueFunc(i)));
            return builder.ToString();
        }
    }
}