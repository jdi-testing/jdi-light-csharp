using System;
using JDI.Light.Elements.Base;

namespace JDI.Light.Exceptions
{
    public class ElementDisabledException : Exception
    {
        public ElementDisabledException(UIElement element) :
            base($"Element {element} with {element.Locator} locator is disabled")
        {
        }
    }
}