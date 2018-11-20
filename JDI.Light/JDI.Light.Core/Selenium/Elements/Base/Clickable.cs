using System;
using JDI.Core.Interfaces.Base;
using JDI.Core.Selenium.Base;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace JDI.Core.Selenium.Elements.Base
{
    public class Clickable : UIElement, IClickable
    {
        public Action<UIElement> ClickAction = cl => cl.WebElement.Click();

        protected Action<UIElement> ClickJsAction =
            cl => cl.JsExecutor.ExecuteScript("arguments[0].click();", cl.WebElement);

        public Clickable() : this(null)
        {
        }

        public Clickable(By byLocator = null, IWebElement webElement = null)
            : base(byLocator, webElement)
        {
        }

        public void Click()
        {
            Actions.Click(ClickAction);
        }

        public void ClickByXY(int x, int y)
        {
            Invoker.DoJAction($"Click on Element with coordinates (x,y) = ({x},{y})",
                el =>
                {
                    new Actions(WebDriver).MoveToElement(WebElement, x, y).Click().Build().Perform();
                }
            );
        }
    }
}