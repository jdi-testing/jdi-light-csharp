using System;
using JDI.Light.Asserts.Generic;
using JDI.Light.Elements.Common;
using JDI.Light.Matchers;
using static JDI.Light.Jdi;

namespace JDI.Light.Asserts
{
    public class ImageAssert : IsAssert<ImageAssert>
    {
        protected Image Image { get; }

        public ImageAssert(Image image) : base(image)
        {
            Image = image;
        }

        public ImageAssert Src(Matcher<string> condition)
        {
            Assert.IsTrue(condition.IsMatch(Image.Src), $"The src value {Image.Src} is not {condition.ActionName} {condition.RightValue}");
            return this;
        }

        public ImageAssert Alt(Matcher<string> condition)
        {
            Assert.IsTrue(condition.IsMatch(Image.Alt), $"The alt value {Image.Alt} is not {condition.ActionName} {condition.RightValue}");
            return this;
        }

        public ImageAssert Height(Matcher<int> condition)
        {
            Assert.IsTrue(condition.IsMatch(Convert.ToInt32(Image.Height)), $"The height value {Image.Height} is not {condition.ActionName} {condition.RightValue}");
            return this;
        }

        public ImageAssert Width(Matcher<int> condition)
        {
            Assert.IsTrue(condition.IsMatch(Convert.ToInt32(Image.Width)), $"The width value {Image.Width} is not {condition.ActionName} {condition.RightValue}");
            return this;
        }
    }
}