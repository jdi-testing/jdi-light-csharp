using System.Drawing;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace JDI.Light.Elements.Interfaces
{
    public interface IBaseElement : IHasValue
    {
        void Hover();
        Point GetLocation();
        Size GetSize();
        Rectangle GetRect();
        Screenshot GetScreenshot();
        string GetAttribute(string name);
        void SetAttribute(string name, string value);
        void Highlight(string color);
        void Highlight();
        void Show();
        SelectElement Select();
    }
}