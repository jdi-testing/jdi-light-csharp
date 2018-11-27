using System;
using JDI.Light.Interfaces;
using JDI.Light.Settings;

namespace JDI.Light.Tests.Asserts
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
            NUnit.Framework.Assert.Fail(message);
            return new Exception(message);
        }

        public void IsTrue(bool actual)
        {
            NUnit.Framework.Assert.IsTrue(actual);
        }
    }
}