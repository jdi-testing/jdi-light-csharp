using System;
using JDI.Core.Interfaces.Base;
using JDI.Core.Selenium.Base;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace JDI.Core.Selenium.Elements.Base
{
    public class Clickable : WebElement, IClickable
    {
        public Action<WebBaseElement> ClickAction = cl => cl.WebElement.Click();

        protected Action<WebBaseElement> ClickJsAction =
            cl => cl.JsExecutor.ExecuteScript("arguments[0].click();", cl.WebElement);

        public Clickable() : this(null)
        {
        }

        public Clickable(By byLocator = null, IWebElement webElement = null, WebBaseElement element = null)
            : base(byLocator, webElement, element)
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
                    var element = WebElement;
                    new Actions(WebDriver).MoveToElement(WebElement, x, y).Click().Build().Perform();
                }
            );
        }
    }
}