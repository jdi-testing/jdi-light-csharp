using System;
using JDI.Light.Settings;

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
                throw JDISettings.Asserter.Exception(msg, ex);
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
                throw JDISettings.Asserter.Exception(msg, ex);
            }
        }

        public static T AvoidExceptions<T>(this Func<T> waitFunc)
        {
            try
            {
                return waitFunc();
            }
            catch
            {
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
    }
}