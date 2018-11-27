using JDI.Light.Matchers;

namespace JDI.Light.Tests.Asserts
{
    public class Check : BaseAsserter
    {
        public Check()
        {
        }

        public Check(string checkMessage) : base(checkMessage)
        {
        }

        public override void ThrowFail(string message)
        {
            NUnit.Framework.Assert.Fail(message);
        }
    }
}