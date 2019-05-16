using JDI.Light.Asserts.Generic;
using JDI.Light.Elements.Common;
using static JDI.Light.Jdi;

namespace JDI.Light.Asserts
{
    public class ColorAssert : IsAssert<ColorAssert>
    {
        protected ColorPicker ColorPicker { get; }

        public ColorAssert(ColorPicker colorPicker) : base(colorPicker)
        {
            ColorPicker = colorPicker;
        }

        public ColorAssert Color(string expectedColor)
        {
            Assert.AreEquals(ColorPicker.Color(), expectedColor);
            return this;
        }
    }
}