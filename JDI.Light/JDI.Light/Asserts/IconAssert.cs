using JDI.Light.Asserts.Generic;
using JDI.Light.Elements.Common;
using JDI.Light.Matchers;
using System;
using static JDI.Light.Jdi;

namespace JDI.Light.Asserts
{
    public class IconAssert : IsAssert<IconAssert>
    {
        protected Icon Icon { get; }

        public IconAssert(Icon icon) : base(icon)
        {
            Icon = icon;
        }

        public IconAssert Src(Matcher<string> condition)
        {
            Assert.IsTrue(condition.IsMatch(Icon.Src), $"The src value {Icon.Src} is not {condition.ActionName} {condition.RightValue}");
            return this;
        }

        public IconAssert Alt(Matcher<string> condition)
        {
            Assert.IsTrue(condition.IsMatch(Icon.Alt), $"The alt value {Icon.Alt} is not {condition.ActionName} {condition.RightValue}");
            return this;
        }

        public IconAssert Height(Matcher<int> condition)
        {
            Assert.IsTrue(condition.IsMatch(Convert.ToInt32(Icon.Height)), $"The height value {Icon.Height} is not {condition.ActionName} {condition.RightValue}");
            return this;
        }

        public IconAssert Width(Matcher<int> condition)
        {
            Assert.IsTrue(condition.IsMatch(Convert.ToInt32(Icon.Width)), $"The width value {Icon.Width} is not {condition.ActionName} {condition.RightValue}");
            return this;
        }
    }
}
