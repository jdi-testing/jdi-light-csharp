using JDI.Light.Interfaces.Base;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace JDI.Light.Elements.Base
{
    public class Clickable : UIElement, IClickable
    {
        public Clickable(By byLocator) : base(byLocator)
        {
        }

        public void Click()
        {
            Invoker.DoActionWithWait("Click on Element", () => WebElement.Click());
        }
    }
}