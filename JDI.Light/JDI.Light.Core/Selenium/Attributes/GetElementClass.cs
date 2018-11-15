using System.Linq;
using JDI.Core.Attributes.Functions;
using JDI.Core.Extensions;
using JDI.Core.Interfaces.Common;
using JDI.Core.Selenium.Base;
using JDI.Core.Selenium.Elements.Common;
using JDI.Core.Settings;
using JDI.Core.Utils;

namespace JDI.Core.Selenium.Attributes
{
    public class GetElementClass
    {
        private readonly WebBaseElement _element;

        public GetElementClass(WebBaseElement element)
        {
            _element = element;
        }

        private static string ToButton(string buttonName)
        {
            return buttonName.ToLower().Contains("button") ? buttonName : buttonName + "Button";
        }

        public Button GetButton(string buttonName)
        {
            var fields = _element.GetFields(typeof(IButton));
            switch (fields.Count)
            {
                case 0:
                    throw JDISettings.Exception($"Can't find ny buttons on form {ToString()}'");
                case 1:
                    return (Button) fields[0].GetValue(_element);
                default:
                    var buttons = fields.Select(f => (Button) f.GetValue(_element)).ToList();
                    var button = buttons.FirstOrDefault(b => ToButton(b.Name).SimplifiedEqual(ToButton(buttonName)));
                    if (button == null)
                        throw JDISettings.Exception($"Can't find button '{buttonName}' for Element '{ToString()}'." +
                                                    $"(Found following buttons: {buttons.Select(el => el.Name).Print()})."
                                                        .FromNewLine());
                    return button;
            }
        }

        public Button GetButton(Functions funcName)
        {
            var fields = _element.GetFields(typeof(IButton));
            if (fields.Count == 1)
                return (Button) fields[0].GetValue(_element);
            var buttons = fields.Select(f => (Button) f.GetValue(_element)).ToList();
            var button = buttons.FirstOrDefault(b => b.Function.Equals(funcName));
            if (button != null) return button;
            var name = funcName.ToString();
            button = buttons.FirstOrDefault(b => ToButton(b.Name).SimplifiedEqual(ToButton(name)));
            if (button == null)
                throw JDISettings.Exception($"Can't find button '{name}' for Element '{ToString()}'");
            return button;
        }

        public Text GetTextElement()
        {
            var textField = this.GetFirstField(typeof(Text), typeof(IText));
            if (textField == null)
                throw JDISettings.Exception($"Can't find Text Element '{ToString()}'");
            return (Text) textField.GetValue(_element);
        }
    }
}