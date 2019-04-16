using JDI.Light.Interfaces.Common;

namespace JDI.Light.Elements.Common
{
    public class Title : TextElement, ITitle
    {
        public void ClickTitle()
        {
            Click();
        }
    }
}