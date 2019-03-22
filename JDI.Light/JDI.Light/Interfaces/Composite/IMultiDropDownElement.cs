using JDI.Light.Elements.Common;
using OpenQA.Selenium;

namespace JDI.Light.Interfaces.Composite
{
    public interface IMultiDropdownElement
    {
        By LabelLocator { get; set; }
        By CheckboxLocator { get; set; }

        Label Label { get; }
        CheckBox CheckBox { get; }
        bool IsSelected { get; }
        bool OptionIsEnabled { get; }

        string Text { get; }
        void Select();
    }
}