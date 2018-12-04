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
            Invoker.DoAction("Click on Element", () => WebElement.Click());
        }

        public void ClickByXY(int x, int y)
        {
            Invoker.DoAction($"Click on Element with coordinates (x,y) = ({x},{y})",
                () =>
                {
                    new Actions(WebDriver).MoveToElement(WebElement, x, y).Click().Build().Perform();
                }
            );
        }
    }
}