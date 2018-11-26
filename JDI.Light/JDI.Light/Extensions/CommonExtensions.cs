using System;
using System.Collections.Generic;
using System.Linq;

namespace JDI.Core.Extensions
{
    public static class CommonExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach (var element in enumerable)
                action(element);
        }

        public static string FormattedJoin(this IEnumerable<string> list, string separator = ", ", string format = "{0}")
        {
            return list != null ? string.Join(separator, list.Select(el => string.Format(format, el))) : "";
        }

        public static string Print<TValue>(this IEnumerable<KeyValuePair<string, TValue>> collection,
            string separator = "; ", string pairFormat = "{0}: {1}")
        {
            return collection != null
                ? string.Join(separator, collection.Select(pair => string.Format(pairFormat, pair.Key, pair.Value)))
                : "";
        }

        public static IList<T> ListCopy<T>(this IList<T> list, int from = 0, int to = 0)
        {
            if (from * to < 0)
                throw new Exception($"from and to should have same sign ({from}, {to})");
            if (from < 0)
                from = list.Count + from - 1;
            if (to <= 0)
                to = list.Count + to - 1;
            var result = new List<T>();
            for (var i = from; i <= to; i++)
                result.Add(list[i]);
            return result;
        }

        public static Dictionary<T, T1> ToDictionary<T, T1>(this IEnumerable<KeyValuePair<T, T1>> pairs)
        {
            return pairs.ToDictionary(el => el.Key, el => el.Value);
        }
    }
}