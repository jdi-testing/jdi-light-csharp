using System;
using System.Collections.Generic;
using System.Linq;
using JDI.Light.Interfaces;

namespace JDI.Light.Utils
{
    public class BaseAsserter : IAssert
    {
        private ILogger _logger;

        public ILogger Logger
        {
            get => _logger ?? (_logger = Jdi.Logger);
            set => _logger = value;
        }

        public virtual void ThrowFail(string message)
        {
        }

        public void ThrowFail(string message, Exception ex)
        {
            throw new Exception(message, ex);
        }

        public Exception Exception(string message)
        {
            throw new Exception(message);
        }

        public void Contains(string actual, string expected)
        {
            var result = actual.Contains(expected);
            AssertAction($"Check that '{actual}' contains '{expected}'", result);
        }

        private void AssertAction(string message, bool result)
        {
            // TODO: Take screenshot
            //TakeScreenshot();
            if (!result)
            {
                AssertException(message + " failed");
            }
        }

        private void AssertException(string failMessage, params object[] args)
        {
            var failMsg = args.Length > 0 ? string.Format(failMessage, args) : failMessage;
            Logger.Error(failMsg);
            ThrowFail(failMsg);
        }

        public void IsTrue(bool condition, string message = "")
        {
            var msg = string.IsNullOrEmpty(message) ? "" : $": {message}";
            AssertAction($"Check that condition is true{msg}", condition);
        }

        public void IsFalse(bool condition, string message = "")
        {
            var msg = string.IsNullOrEmpty(message) ? "" : $": {message}";
            AssertAction($"Check that condition is false{msg}", !condition);
        }

        public void AreEquals<T>(T actual, T expected)
        {
            var result = typeof(T) == typeof(string) ? actual.ToString().Equals(expected.ToString()) : actual.Equals(expected);
            AssertAction($"Check that '{actual}' equals to '{expected}'", result);
        }

        public void CollectionEquals<T>(IEnumerable<T> actual, IEnumerable<T> expected)
        {
            var first = actual as T[] ?? actual.ToArray();
            var second = expected as T[] ?? expected.ToArray();
            var result = first.OrderBy(i => i).SequenceEqual(second.OrderBy(i => i));
            AssertAction($"Check that collection '{string.Join(", ", first)}' equals to '{string.Join(", ", second)}'",
                result);
        }
    }
}