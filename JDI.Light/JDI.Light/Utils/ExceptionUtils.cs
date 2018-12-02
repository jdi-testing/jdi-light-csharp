using System;

namespace JDI.Light.Utils
{
    public static class ExceptionUtils
    {
        public static void ActionWithException(Action action, Func<string, string> getExceptionMsg)
        {
            try
            {
                action.Invoke();
            }
            catch (Exception ex)
            {
                var msg = getExceptionMsg.Invoke(ex.Message);
                JDI.Assert.ThrowFail(msg, ex);
            }
        }

        public static T ActionWithException<T>(Func<T> func, Func<string, string> getExceptionMsg)
        {
            try
            {
                var res = func.Invoke();
                return res;
            }
            catch (Exception ex)
            {
                var msg = getExceptionMsg.Invoke(ex.Message);
                JDI.Assert.ThrowFail(msg, ex);
                return default(T);
            }
        }

        public static T AvoidExceptions<T>(this Func<T> waitFunc)
        {
            try
            {
                return waitFunc();
            }
            catch(Exception e)
            {
                var ex = e;
                return default(T);
            }
        }

        public static void AvoidExceptions(this Action action)
        {
            try
            {
                action();
            }
            catch
            {
                /* ignored */
            }
        }

        public static void GetFromPropertiesAvoidExceptions(Action<string> action, string name)
        {
            AvoidExceptions(() => action.Invoke(Properties.Settings.Default[name].ToString()));
        }
    }
}