using System.Collections.Generic;

namespace JDI.Light.Tests.DataProviders
{
    internal class IndexesProvider
    {
        public static IEnumerable<int> Indexes
        {
            get
            {
                yield return -10;
                yield return 0;
                yield return 10;
            }
        }
    }
}