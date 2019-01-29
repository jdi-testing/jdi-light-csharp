using OpenQA.Selenium;

namespace JDI.Light.Interfaces.Base
{
    public interface IBaseUIElement : IBaseElement, IWebElement
    {
        IBaseElement Parent { get; set; }
        void SetAttribute(string attributeName, string value);
        void Highlight(string borderColor, string backgroundColor, int highlightMillisecondsTime);
    }
}