using System;
using System.Collections.Generic;

namespace ValidationProject.Extensions
{
    public static class CollectionExtensions
    {
        public static ICollection<T> AddRange<T>(this ICollection<T> collection, IEnumerable<T> items)
        {
            items.ForEach(collection.Add);
            return collection;
        }

        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (T obj in source)
                action(obj);
        }
    }
}
