using JDI.Light.Elements.Common;
using JDI.Light.Asserts.Generic;
using JDI.Light.Matchers;
using static JDI.Light.Jdi;

namespace JDI.Light.Asserts
{
    public class ComboBoxAssert : IsAssert<ComboBoxAssert>
    {
        protected ComboBox ComboBox { get; }

        public ComboBoxAssert(ComboBox comboBox) : base(comboBox)
        {
            ComboBox = comboBox;
        }

        public ComboBoxAssert Selected(Matcher<string> condition)
        {
            Assert.IsTrue(condition.IsMatch(ComboBox.Selected()), $"The selected value {ComboBox.Selected()} is not {condition.ActionName} {condition.RightValue}");
            return this;
        }
    }
}
