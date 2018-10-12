using System.Drawing;
using OpenQA.Selenium.Support.UI;

namespace JDI.Light.Elements.Interfaces
{
    public interface IBaseElement : IHasValue
    {
        void Hover();
        bool IsEnabled();
        bool IsDisplayed();
        Point GetLocation();
        Size GetSize();
        Rectangle GetRect();
        T GetScreenshotAs<T>(T outputType);
        string GetAttribute(string name);
        void SetAttribute(string name, string value);
        void Highlight(string color);
        void Highlight();
        void Show();
        SelectElement Select();
    }
}