using System;
using JDI.Light.Utils;

namespace JDI.Light.Extensions
{
    public static class TimerExtensions
    {
        public static T GetByCondition<T>(this Func<T> getFunc, Func<T, bool> conditionFunc)
        {
            return new Timer().GetResultByCondition(getFunc, conditionFunc);
        }
    }
}