using System;

namespace JDI.Commons
{
    public static class ExceptionUtils
    {
        public static T AvoidExceptions<T>(this Func<T> waitFunc)
        {
            try { return waitFunc(); }
            catch { return default(T); }
        }
    }
}
