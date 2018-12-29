using System;
using System.Linq;
using JDI.Light.Elements.Base;
using JDI.Light.Extensions;
using JDI.Light.Interfaces.Base;
using JDI.Light.Interfaces.Composite;
using JDI.Light.Utils;
using OpenQA.Selenium;

namespace JDI.Light.Elements.Composite
{
    public class Pagination : UIElement, IPagination
    {
        public Func<Pagination, UIElement> FirstAction = p =>
        {
            const string shortName = "first";
            if (p.FirstLocator != null)
                return new UIElement(p.FirstLocator);

            var firstLink = p.GetClickable(shortName);
            if (firstLink != null)
                return firstLink;

            if (p.Locator != null && p.Locator.ToString().Contains("{0}"))
                return new UIElement(p.Locator.FillByTemplate(shortName));
            throw JDI.Assert.Exception(p.CantChooseElementMsg("First", shortName, "firstAction"));
        };

        public By FirstLocator;

        public Func<Pagination, UIElement> LastAction = p =>
        {
            const string shortName = "last";
            if (p.LastLocator != null)
                return new UIElement(p.LastLocator);

            var lastLink = p.GetClickable(shortName);
            if (lastLink != null)
                return lastLink;

            if (p.Locator != null && p.Locator.ToString().Contains("{0}"))
                return new UIElement(p.Locator.FillByTemplate(shortName));
            throw JDI.Assert.Exception(p.CantChooseElementMsg("Last", shortName, "lastAction"));
        };

        public By LastLocator;

        public Func<Pagination, UIElement> NextAction = p =>
        {
            const string shortName = "next";
            if (p.NextLocator != null)
                return new UIElement(p.NextLocator);

            var nextLink = p.GetClickable(shortName);
            if (nextLink != null)
                return nextLink;

            if (p.Locator != null && p.Locator.ToString().Contains("{0}"))
                return new UIElement(p.Locator.FillByTemplate(shortName));
            throw JDI.Assert.Exception(p.CantChooseElementMsg("Next", shortName, "nextAction"));
        };

        public By NextLocator;

        public Func<Pagination, int, UIElement> PageAction = (p, index) =>
        {
            const string shortName = "page";
            if (p.Locator != null && p.Locator.ToString().Contains("{0}"))
                return new UIElement(p.Locator.FillByTemplate(index));

            var pageLink = p.GetClickable(shortName);
            if (pageLink != null)
                return pageLink;
            throw JDI.Assert.Exception(p.CantChooseElementMsg(index.ToString(), shortName, "pageAction"));
        };

        public Func<Pagination, UIElement> PreviousAction = p =>
        {
            const string shortName = "prev";
            if (p.PreviousLocator != null)
                return new UIElement(p.PreviousLocator);

            var prevLink = p.GetClickable(shortName);
            if (prevLink != null)
                return prevLink;

            if (p.Locator != null && p.Locator.ToString().Contains("{0}"))
                return new UIElement(p.Locator.FillByTemplate(shortName));
            throw JDI.Assert.Exception(p.CantChooseElementMsg("Previous", shortName, "previousAction"));
        };

        public By PreviousLocator;

        public Pagination(By pageTemplateLocator = null, By nextLocator = null, By previousLocator = null,
            By firstLocator = null, By lastLocator = null)
            : base(pageTemplateLocator)
        {
            NextLocator = nextLocator;
            PreviousLocator = previousLocator;
            FirstLocator = firstLocator;
            LastLocator = lastLocator;
        }

        public void Next()
        {
            Invoker.DoActionWithWait("Choose Next page", () => NextAction(this).Click());
        }

        public void Previous()
        {
            Invoker.DoActionWithWait("Choose Previous page", () => PreviousAction(this).Click());
        }

        public void First()
        {
            Invoker.DoActionWithWait("Choose First page", () => FirstAction(this).Click());
        }

        public void Last()
        {
            Invoker.DoActionWithWait("Choose Last page", () => LastAction(this).Click());
        }

        public void SelectPage(int index)
        {
            Invoker.DoActionWithWait($"Choose '{index}' page", () => PageAction(this, index).Click());
        }

        private UIElement GetClickable(string name)
        {
            var fields = this.GetFields(typeof(IBaseUIElement));
            var result = fields.FirstOrDefault(field =>
                field.GetElementName().ToLower().Contains(name.ToLower()));
            return (UIElement) result?.GetValue(this);
        }

        private string CantChooseElementMsg(string actionName, string shortName, string action)
        {
            return $@"Can't choose {actionName} page for Element '{ToString()}'. 
Please specify locator for this action using constructor or add Clickable Element on pageObject 
with name '{shortName}Link' or '{shortName}Button' or use locator template with parameter '{shortName}' 
or override {action}() in class";
        }
    }
}