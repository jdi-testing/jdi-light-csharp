using JDI.Light.Elements.Common;
using static JDI.Light.Jdi;

namespace JDI.Light.Asserts
{
    public class CheckBoxAssert
    {
        protected CheckBox CheckBox { get; }

        public CheckBoxAssert(CheckBox checkBox)
        {
            CheckBox = checkBox;
        }

        public CheckBoxAssert Selected()
        {
            Assert.IsTrue(CheckBox.IsChecked);
            return this;
        }

        public CheckBoxAssert Deselected()
        {
            Assert.IsFalse(CheckBox.IsChecked);
            return this;
        }

        public CheckBoxAssert Enabled()
        {
            Assert.IsTrue(CheckBox.Enabled);
            return this;
        }

        public CheckBoxAssert Displayed()
        {
            Assert.IsTrue(CheckBox.Displayed);
            return this;
        }
    }
}
