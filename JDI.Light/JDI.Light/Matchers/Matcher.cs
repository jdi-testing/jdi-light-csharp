using System;
using System.Collections.Generic;

namespace JDI.Light.Matchers
{
    public abstract class Matcher<T>
    {
        protected Matcher(T rightValue)
        {
            RightValue = rightValue;
        }

        public T RightValue { get; }

        public T LeftValue { get; private set; }

        public abstract string ActionName { get; }

        public string FailedMessage()
        {
            if (typeof(IEnumerable<object>).IsAssignableFrom(typeof(T)))
            {
                return
                    $"{string.Join(",", LeftValue as IEnumerable<object>)} are not {ActionName} {string.Join(",", RightValue as IEnumerable<object>)}";
            }
            return $"{LeftValue} is not {ActionName} {RightValue}";
        }

        protected abstract Func<T, T, bool> Condition { get; }

        public bool IsMatch(T leftValue)
        {
            LeftValue = leftValue;
            return Condition(leftValue, RightValue);
        } 
    }
}