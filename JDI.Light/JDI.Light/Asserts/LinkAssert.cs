using JDI.Light.Elements.Common;
using JDI.Light.Matchers;
using static JDI.Light.Jdi;

namespace JDI.Light.Asserts
{
    public class LinkAssert : TextAssert
    {
        protected Link Link { get; }

        public LinkAssert(Link link) : base(link)
        {
            Link = link;
        }

        public LinkAssert Ref(Matcher<string> condition)
        {
            Assert.IsTrue(condition.IsMatch(Link.Ref()),
                $"The link reference {Link.Ref()} is not {condition.ActionName} {condition.RightValue}");
            return this;
        }

        public LinkAssert Alt(Matcher<string> condition)
        {
            Assert.IsTrue(condition.IsMatch(Link.Alt()),
                $"The alt {Link.Alt()} is not {condition.ActionName} {condition.RightValue}");
            return this;
        }
    }
}