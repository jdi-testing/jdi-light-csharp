using System.Collections.Generic;
using JDI.Light.Matchers.CollectionMatchers;
using JDI.Light.Matchers.IntegerMachers;

namespace JDI.Light.Matchers
{
    public static class Is
    {
        public static GreaterThan GreaterThan(int rightNumber) => new GreaterThan(rightNumber);

        public static GreaterThanOrEqualTo GreaterThanOrEqualTo(int rightNumber) =>
            new GreaterThanOrEqualTo(rightNumber);

        public static LessThan LessThan(int rightNumber) => new LessThan(rightNumber);

        public static LessThanOrEqualTo LessThanOrEqualTo(int rightNumber) => new LessThanOrEqualTo(rightNumber);

        public static SubsequenceOf<T> SubsequenceOf<T>(IEnumerable<T> rightSubsequence) =>
            new SubsequenceOf<T>(rightSubsequence);
    }
}
