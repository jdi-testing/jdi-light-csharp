using System;
using JDI.Light.Elements.Base;
using OpenQA.Selenium;
using static JDI.Light.Jdi;

namespace JDI.Light.Elements.Composite
{
    public class Section : UIElement
    {
        public Section() : base(null)
        {
        }

        public Section(By locator) : base(locator)
        {
        }

        public void CheckInitializedElement(UIElement htmlElementToCheck, string expectedLocator, object expectedParent, string expectedName)
        {
            if (htmlElementToCheck != null)
            {
                Assert.AreEquals(htmlElementToCheck.Locator.ToString(), expectedLocator);
                Assert.AreEquals(htmlElementToCheck.Parent, expectedParent);
                Assert.AreEquals(htmlElementToCheck.Name, expectedName);
            }
            else
            {
                throw new ArgumentNullException($"{expectedName} element is null.");
            }
        }
    }
}