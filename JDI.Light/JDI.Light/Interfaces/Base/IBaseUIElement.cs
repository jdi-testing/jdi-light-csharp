using OpenQA.Selenium;

namespace JDI.Light.Interfaces.Base
{
    public interface IBaseUIElement : IBaseElement, IWebElement
    {
        IBaseElement Parent { get; set; }
        bool OnlyOneElementAllowedInSearch { get; set; }
        By Locator { get; set; }
        IWebElement WebElement { get; set; }
        void SetAttribute(string attributeName, string value);
        void Highlight(string borderColor, string backgroundColor, int highlightMillisecondsTime);
        void Show();
    }
}