using System.Collections.Generic;
using System.Linq;
using JDI.Light.Elements.Common;
using JDI.Light.Extensions;
using JDI.Light.Interfaces.Common;
using JDI.Light.Utils;
using OpenQA.Selenium;

namespace JDI.Light.Elements.Base
{
    public class CompositeUIElement : UIElement
    {
        public CompositeUIElement(By locator) : base(locator)
        {
        }
    }
}