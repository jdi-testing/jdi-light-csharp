using System;

namespace JDI.Light.Matchers
{
    public abstract class Matcher<T>
    {
        protected Matcher(T rightValue)
        {
            RightValue = rightValue;
        }

        public T RightValue { get; }

        public abstract string ActionName { get; }

        protected abstract Func<T, T, bool> Condition { get; }

        public bool IsMatch(T leftValue) => Condition(leftValue, RightValue);
    }
}