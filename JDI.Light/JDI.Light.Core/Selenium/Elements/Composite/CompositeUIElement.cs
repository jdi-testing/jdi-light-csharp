using System.Collections.Generic;
using System.Linq;
using JDI.Core.Extensions;
using JDI.Core.Interfaces.Common;
using JDI.Core.Selenium.Elements.Base;
using JDI.Core.Selenium.Elements.Common;
using JDI.Core.Settings;
using JDI.Core.Utils;
using OpenQA.Selenium;

namespace JDI.Core.Selenium.Elements.Composite
{
    public class CompositeUIElement : UIElement
    {
        public CompositeUIElement() : base()
        {
        }

        public CompositeUIElement(By locator) : base(locator)
        {
        }

        public CompositeUIElement(By locator, IWebElement webElement) : base(locator, webElement)
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
                JDISettings.Logger.Debug($"Get Web Elements: {this}");
                var elements = GetWebElementsAction();
                JDISettings.Logger.Debug($"Found {elements.Count} elements");
                return elements;
            }
        }

        public Button GetButton(string buttonName)
        {
            var fields = WebElement.GetFields(typeof(IButton));
            switch (fields.Count)
            {
                case 0:
                    throw JDISettings.Exception($"Can't find ny buttons on form {ToString()}'");
                case 1:
                    return (Button)fields[0].GetValue(WebElement);
                default:
                    var buttons = fields.Select(f => (Button)f.GetValue(WebElement)).ToList();
                    var button = buttons.FirstOrDefault(b => ToButton(b.Name).SimplifiedEqual(ToButton(buttonName)));
                    if (button == null)
                        throw JDISettings.Exception($"Can't find button '{buttonName}' for Element '{ToString()}'." +
                                                    $"(Found following buttons: {buttons.Select(el => el.Name).Print()})."
                                                        .FromNewLine());
                    return button;
            }
        }
    }
}