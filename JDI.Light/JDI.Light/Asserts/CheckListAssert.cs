using System.Collections.Generic;
using JDI.Light.Elements.Complex;
using JDI.Light.Matchers;
using static JDI.Light.Jdi;

namespace JDI.Light.Asserts
{
    public class CheckListAssert
    {
        protected CheckList CheckList { get; }

        public CheckListAssert(CheckList checkList)
        {
            CheckList = checkList;
        }

        public CheckListAssert Selected(Matcher<IEnumerable<string>> condition)
        {
            Assert.IsTrue(condition.IsMatch(CheckList.Checked()),
                $"available values {condition.FailedMessage()}");
            return this;
        }
    }
}
