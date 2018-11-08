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

        public static void AvoidExceptions(this Action action)
        {
            try { action(); }
            catch { /* ignored */ }
        }

        public static string AvoidExceptions(Func<object> p)
        {
            throw new NotImplementedException();
        }
    }
}
