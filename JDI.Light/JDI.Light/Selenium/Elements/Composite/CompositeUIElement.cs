using System.Collections.Generic;
using System.Linq;
using JDI.Light.Extensions;
using JDI.Light.Interfaces.Common;
using JDI.Light.Selenium.Elements.Base;
using JDI.Light.Selenium.Elements.Common;
using JDI.Light.Settings;
using JDI.Light.Utils;
using OpenQA.Selenium;

namespace JDI.Light.Selenium.Elements.Composite
{
    public class CompositeUIElement : UIElement
    {
        public CompositeUIElement() : base(null)
        {
        }

        public CompositeUIElement(By locator) : base(locator)
        {
        }

        private string ToButton(string buttonName)
        {
            return buttonName.ToLower().Contains("button") ? buttonName : buttonName + "Button";
        }

        public List<IWebElement> WebElements
        {
            get
            {
                WebSettings.Logger.Debug($"Get Web Elements: {this}");
                var elements = GetWebElementsAction();
                WebSettings.Logger.Debug($"Found {elements.Count} elements");
                return elements;
            }
        }

        public Button GetButton(string buttonName)
        {
            var fields = WebElement.GetFields(typeof(IButton));
            switch (fields.Count)
            {
                case 0:
                    throw WebSettings.Assert.Exception($"Can't find ny buttons on form {ToString()}'");
                case 1:
                    return (Button)fields[0].GetValue(WebElement);
                default:
                    var buttons = fields.Select(f => (Button)f.GetValue(WebElement)).ToList();
                    var button = buttons.FirstOrDefault(b => ToButton(b.Name).SimplifiedEqual(ToButton(buttonName)));
                    if (button == null)
                        throw WebSettings.Assert.Exception($"Can't find button '{buttonName}' for Element '{ToString()}'." +
                                                    $"(Found following buttons: {buttons.Select(el => el.Name).FormattedJoin()})."
                                                        .FromNewLine());
                    return button;
            }
        }
    }
}