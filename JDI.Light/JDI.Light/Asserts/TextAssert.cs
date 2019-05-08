using JDI.Light.Asserts.Generic;
using JDI.Light.Elements.Common;

namespace JDI.Light.Asserts
{
    public class TextAssert : IsAssert<TextAssert>
    {
        protected TextElement TextElement { get; }

        public TextAssert(TextElement text) : base(text)
        {
            TextElement = text;
        }
    }
}