using JDI.Core.Interfaces.Base;
using JDI.Core.Selenium.Elements.Composite;
using JDI.Core.Selenium.Elements.WebActions;
using OpenQA.Selenium;

namespace JDI.Core.Interfaces.Complex
{
    public interface IPage : IBaseElement
    {
        CheckPageTypes CheckTitleType { get; set; }
        CheckPageTypes CheckUrlType { get; set; }

        string Title { get; set; }
        string UrlTemplate { get; set; }

        ActionInvoker<WebPage> Invoker { get; set; }
        IWebDriver WebDriver { get; set; }
        Timer Timer { get; set; }

        void CheckOpened();
        void Open();
    }
}