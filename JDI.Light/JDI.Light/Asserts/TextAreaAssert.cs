using JDI.Light.Elements.Common;
using JDI.Light.Matchers;
using static JDI.Light.Jdi;

namespace JDI.Light.Asserts
{
    public class TextAreaAssert : TextAssert
    {
        protected TextArea TextArea { get; }

        public TextAreaAssert(TextArea textArea) : base(textArea)
        {
            TextArea = textArea;
        }

        public TextAreaAssert RowsCount(Matcher<int> condition)
        {
            Assert.IsTrue(condition.IsMatch(TextArea.Rows()), $"The rows number {TextArea.Rows()} is not {condition.ActionName} {condition.RightValue}");
            return this;
        }
        public TextAreaAssert ColsCount(Matcher<int> condition)
        {
            Assert.IsTrue(condition.IsMatch(TextArea.Cols()), $"The cols number {TextArea.Cols()} is not {condition.ActionName} {condition.RightValue}");
            return this;
        }
        public TextAreaAssert MinLength(Matcher<int> condition)
        {
            Assert.IsTrue(condition.IsMatch(TextArea.MinLength()), $"The min length {TextArea.MinLength()} is not {condition.ActionName} {condition.RightValue}");
            return this;
        }
        public TextAreaAssert MaxLength(Matcher<int> condition)
        {
            Assert.IsTrue(condition.IsMatch(TextArea.MaxLength()), $"The max length {TextArea.MaxLength()} is not {condition.ActionName} {condition.RightValue}");
            return this;
        }
    }
}