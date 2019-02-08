using System.Collections.Generic;
using System.Linq;

namespace JDI.Light.Extensions
{
    public static class CommonExtensions
    {
        public static string FormattedJoin(this IEnumerable<string> list, string separator = ", ", string format = "{0}")
        {
            return list != null ? string.Join(separator, list.Select(el => string.Format(format, el))) : "";
        }
    }
}