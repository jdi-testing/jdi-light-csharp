using JDI.Light.Interfaces.Base;

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

        public void ClickByXY(int x, int y)
        {
            Invoker.DoActionWithWait($"Click on Element with coordinates (x,y) = ({x},{y})",
                () =>
                {
                    new Actions(WebDriver).MoveToElement(WebElement, x, y).Click().Build().Perform();
                }
            );
        }
    }
}