using OpenQA.Selenium;

namespace JDI.Light.Interfaces.Base
{
    public interface IBaseUIElement : IBaseElement//, IWebElement
    {
        string GetAttribute(string name);
        void SetAttribute(string attributeName, string value);
        void Highlight(string borderColor, string backgroundColor, int highlightMillisecondsTime);
        void Click();
    }
}