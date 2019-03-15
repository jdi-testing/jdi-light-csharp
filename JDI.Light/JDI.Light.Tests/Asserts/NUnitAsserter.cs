using JDI.Light.Utils;

namespace JDI.Light.Tests.Asserts
{
    public class NUnitAsserter : BaseAsserter
    {
        public NUnitAsserter()
        {
        }

        public override void ThrowFail(string message)
        {
            Jdi.Logger.Error(message);
            NUnit.Framework.Assert.Fail(message);
        }
    }
}