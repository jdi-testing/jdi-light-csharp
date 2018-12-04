using System;
using JDI.Light.Elements.Common;
using JDI.Light.Interfaces.Complex;
using OpenQA.Selenium;

namespace JDI.Light.Elements.Complex
{
    public class ComboBox : ComboBox<IConvertible>, IComboBox
    {
    }

    public class ComboBox<TEnum> : Dropdown<TEnum>, IComboBox<TEnum>
        where TEnum : IConvertible
    {
        public Action<ComboBox<TEnum>, string> InputAction =
            (c, text) => c.TextField.SendKeys(text);

        public ComboBox() : this(null)
        {
        }

        public ComboBox(By selectorLocator = null, By optionsNamesLocatorTemplate = null)
            : base(selectorLocator, optionsNamesLocatorTemplate)
        {
        }

        public ComboBox(By selectorLocator, By optionsNamesLocatorTemplate, By valueLocator,
            By allOptionsNamesLocator = null)
            : base(selectorLocator, optionsNamesLocatorTemplate, allOptionsNamesLocator)
        {
            TextField = new TextField(valueLocator);
        }

        public override Func<Dropdown<TEnum>, string> GetTextAction => c => TextField.GetText;

        public override Action<BaseSelector<TEnum>, string> SetValueAction => (c, value) => NewInput(value);

        public TextField TextField { get; set; }

        public void Input(string text)
        {
            Invoker.DoAction($"Input text '{text}' in combobox", () => InputAction(this, text));
        }

        public void SendKeys(string text)
        {
            Input(text);
        }

        public void NewInput(string text)
        {
            Clear();
            Input(text);
        }

        public void Clear()
        {
            Invoker.DoAction("Clear combobox", ClearAction);
        }

        public void Focus()
        {
            Invoker.DoAction("Focus on text field", FocusAction);
        }

        public void ClearAction()
        {
            TextField.Clear();
        }

        public void FocusAction()
        {
            TextField.Focus();
        }
    }
}