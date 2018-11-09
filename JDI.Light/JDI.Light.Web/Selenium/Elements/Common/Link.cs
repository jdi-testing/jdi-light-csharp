using System;
using JDI.Core;
using JDI.Core.Interfaces.Common;
using JDI.Matchers;
using JDI.Web.Selenium.Base;
using JDI.Web.Selenium.Elements.Base;
using OpenQA.Selenium;

namespace JDI.Web.Selenium.Elements.Common
{
    public class Link : ClickableText, ILink
    {
        protected Func<WebBaseElement, string> GetReferenceFunc =
            el => el.WebAvatar.FindImmediately(() => el.WebElement.GetAttribute("href"), "");

        protected Func<WebBaseElement, string> GetTooltipFunc =
            el => el.WebAvatar.FindImmediately(() => el.WebElement.GetAttribute("title"), "");

        public Link() : this(null)
        {
        }

        public Link(By byLocator = null, IWebElement webElement = null, WebBaseElement element = null)
            : base(byLocator, webElement, element)
        {
        }

        public string GetReference()
        {
            return Invoker.DoJActionResult("Get link reference", GetReferenceFunc,
                href => $"Get href of link '{href}'");
        }

        public string WaitReferenceContains(string text)
        {
            Func<WebBaseElement, Func<string>> textFunc = el => () => GetReferenceFunc(el);
            return Invoker.DoJActionResult(
                $"Wait link contains '{text}'",
                el => textFunc(el).GetByCondition(t => t.Contains(text))
            );
        }

        public string WaitMatchReference(string regEx)
        {
            Func<WebBaseElement, Func<string>> textFunc = el => () => GetReferenceFunc(el);
            return Invoker.DoJActionResult(
                $"Wait link match regex '{regEx}'",
                el => textFunc(el).GetByCondition(t => t.Matches(regEx))
            );
        }

        public string GetTooltip()
        {
            return Invoker.DoJActionResult("Get link tooltip", GetTooltipFunc, href => $"Get link tooltip '{href}'");
        }
    }
}