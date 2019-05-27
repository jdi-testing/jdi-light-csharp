using JDI.Light.Elements.Common;
using JDI.Light.Asserts.Generic;

namespace JDI.Light.Asserts
{
    public class ComboBoxAssert : IsAssert<ComboBoxAssert>
    {
        protected ComboBox ComboBox { get; }

        public ComboBoxAssert(ComboBox comboBox) : base(comboBox)
        {
            ComboBox = comboBox;
        }
    }
}
