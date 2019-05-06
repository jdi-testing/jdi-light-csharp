using JDI.Light.Elements.Base;
using JDI.Light.Interfaces.Common;
using OpenQA.Selenium;

namespace JDI.Light.Elements.Common
{
    public class ColorPicker : UIElement, IColorPicker
    {
        protected ColorPicker(By byLocator) : base(byLocator)
        {
        }

        public string Color()
        {
            return GetAttribute("value");
        }

        public void SetColor(string color, bool checkEnabled = true)
        {
            CheckEnabled(checkEnabled);
            SetAttribute("value", color);
        }
    }
}