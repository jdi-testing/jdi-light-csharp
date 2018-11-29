using JDI.Light.Matchers;

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
            JDI.Logger.Error(message);
            NUnit.Framework.Assert.Fail(message);
        }
    }
}