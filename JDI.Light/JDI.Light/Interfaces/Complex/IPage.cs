using JDI.Light.Enums;
using JDI.Light.Interfaces.Base;
using JDI.Light.Selenium.Elements.Composite;
using JDI.Light.Selenium.Elements.WebActions;
using JDI.Light.Utils;
using OpenQA.Selenium;

namespace JDI.Light.Interfaces.Complex
{
    public interface IPage : IBaseElement
    {
        CheckPageType CheckTitleType { get; set; }
        CheckPageType CheckUrlType { get; set; }

        string Title { get; set; }
        string UrlTemplate { get; set; }

        ActionInvoker<WebPage> Invoker { get; set; }
        IWebDriver WebDriver { get; set; }
        Timer Timer { get; set; }

        void CheckOpened();
        void Open();
    }
}