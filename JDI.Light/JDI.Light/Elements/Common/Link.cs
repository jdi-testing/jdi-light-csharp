using System;
using JDI.Light.Elements.Base;
using JDI.Light.Interfaces.Common;
using OpenQA.Selenium;

namespace JDI.Light.Elements.Common
{
    public class Link : Text, ILink
    {
        public Link() : this(null)
        {
        }

        public Link(By byLocator = null)
            : base(byLocator)
        {
        }

        public string GetReference()
        {
            return Invoker.DoActionWithResult("Get link reference", () => FindImmediately(() => WebElement.GetAttribute("href"), ""),
                href => $"Get href of link '{href}'");
        }

        public string WaitReferenceContains(string text)
        {
            Func<string> TextFunc (UIElement el) => GetReference;
            return Invoker.DoActionWithResult(
                $"Wait link contains '{text}'",
                () => Timer.GetResultByCondition(TextFunc(this), t => t.Contains(text))
            );
        }

        public string WaitMatchReference(string regEx)
        {
            Func<string> TextFunc(UIElement el) => GetReference;
            return Invoker.DoActionWithResult(
                $"Wait link match regex '{regEx}'",
                () => Timer.GetResultByCondition(TextFunc(this), t => t.Contains(regEx))
            );
        }

        public string GetTooltip()
        {
            return Invoker.DoActionWithResult("Get link tooltip", 
                () => FindImmediately(() => WebElement.GetAttribute("title"), ""), href => $"Get link tooltip '{href}'");
        }
    }
}