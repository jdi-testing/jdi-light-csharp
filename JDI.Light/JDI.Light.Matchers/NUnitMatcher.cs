using System;
using JDI.Core.Settings;
using NUnit.Framework;

namespace JDI.Matchers
{
    public class NUnitMatcher : IAssert
    {
        public Exception Exception(string message, Exception ex)
        {
            JDISettings.Logger.Exception(ex);
            return ex;
        }

        public Exception Exception(string message)
        {
            JDISettings.Logger.Error(message);
            Assert.Fail(message);
            return new Exception(message);
        }

        public void IsTrue(bool actual)
        {
            Assert.IsTrue(actual);
        }
    }
}