using JDI.Light.Asserts.Generic;
using JDI.Light.Elements.Complex;
using JDI.Light.Interfaces;
using JDI.Light.Matchers;
using static JDI.Light.Jdi;

namespace JDI.Light.Asserts
{
    public class RadioButtonAssert : IsAssert<RadioButtonAssert>
    {
        protected RadioButtons RadioButton { get; }

        public RadioButtonAssert(RadioButtons radioButton) : base(radioButton)
        {
            RadioButton = radioButton;
        }

        public RadioButtonAssert Selected(Matcher<string> condition)
        {
            Assert.IsTrue(condition.IsMatch(RadioButton.Value), $"The selected value {RadioButton.Value} is not {condition.ActionName} {condition.RightValue}");
            return this;
        }
    }
}
