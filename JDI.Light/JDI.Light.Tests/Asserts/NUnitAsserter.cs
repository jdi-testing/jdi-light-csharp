using JDI.Light.Matchers;
using JDI.Light.Settings;

namespace JDI.Light.Tests.Asserts
{
    public class NUnitAsserter : BaseAsserter
    {
        public NUnitAsserter()
        {
        }

        public NUnitAsserter(string checkMessage) : base(checkMessage)
        {
        }

        public override void ThrowFail(string message)
        {
            WebSettings.Logger.Error(message);
            NUnit.Framework.Assert.Fail(message);
        }
    }
}