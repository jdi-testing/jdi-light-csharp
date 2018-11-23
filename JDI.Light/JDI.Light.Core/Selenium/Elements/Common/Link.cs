using System;
using JDI.Core.Extensions;
using JDI.Core.Interfaces.Common;
using JDI.Core.Selenium.Elements.Base;
using OpenQA.Selenium;

namespace JDI.Core.Selenium.Elements.Common
{
    public class Link : ClickableText, ILink
    {
        protected Func<UIElement, string> GetReferenceFunc =
            el => el.FindImmediately(() => el.WebElement.GetAttribute("href"), "");

        protected Func<UIElement, string> GetTooltipFunc =
            el => el.FindImmediately(() => el.WebElement.GetAttribute("title"), "");

        public Link() : this(null)
        {
        }

        public Link(By byLocator = null)
            : base(byLocator)
        {
        }

        public string GetReference()
        {
            return Invoker.DoJActionResult("Get link reference", GetReferenceFunc,
                href => $"Get href of link '{href}'");
        }

        public string WaitReferenceContains(string text)
        {
            Func<UIElement, Func<string>> textFunc = el => () => GetReferenceFunc(el);
            return Invoker.DoJActionResult(
                $"Wait link contains '{text}'",
                el => textFunc(el).GetByCondition(t => t.Contains(text))
            );
        }

        public string WaitMatchReference(string regEx)
        {
            Func<UIElement, Func<string>> textFunc = el => () => GetReferenceFunc(el);
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