using JDI.Core.Matchers;

namespace JDI.Light.Tests.Asserts
{
    public class Check : BaseMatcher
    {
        public Check()
        {
        }

        public Check(string checkMessage) : base(checkMessage)
        {
        }

        protected override void ThrowFail(string message)
        {
            global::NUnit.Framework.Assert.Fail(message);
        }
    }
}