using System;
using System.Linq;
using JDI.Core.Attributes;
using JDI.Core.Extensions;
using JDI.Core.Interfaces.Base;
using JDI.Core.Interfaces.Complex;
using JDI.Core.Selenium.DriverFactory;
using JDI.Core.Selenium.Elements.Base;
using JDI.Core.Settings;
using JDI.Core.Utils;
using OpenQA.Selenium;

namespace JDI.Core.Selenium.Elements.Composite
{
    public class Pagination : UIElement, IPagination
    {
        public Func<Pagination, Clickable> FirstAction = p =>
        {
            const string shortName = "first";
            if (p.FirstLocator != null)
                return new Clickable(p.FirstLocator);

            var firstLink = p.GetClickable(shortName);
            if (firstLink != null)
                return firstLink;

            if (p.Locator != null && p.Locator.ToString().Contains("{0}"))
                return new Clickable(p.Locator.FillByTemplate(shortName));
            throw JDISettings.Exception(p.CantChooseElementMsg("First", shortName, "firstAction"));
        };

        public By FirstLocator;

        public Func<Pagination, Clickable> LastAction = p =>
        {
            const string shortName = "last";
            if (p.LastLocator != null)
                return new Clickable(p.LastLocator);

            var lastLink = p.GetClickable(shortName);
            if (lastLink != null)
                return lastLink;

            if (p.Locator != null && p.Locator.ToString().Contains("{0}"))
                return new Clickable(p.Locator.FillByTemplate(shortName));
            throw JDISettings.Exception(p.CantChooseElementMsg("Last", shortName, "lastAction"));
        };

        public By LastLocator;

        public Func<Pagination, Clickable> NextAction = p =>
        {
            const string shortName = "next";
            if (p.NextLocator != null)
                return new Clickable(p.NextLocator);

            var nextLink = p.GetClickable(shortName);
            if (nextLink != null)
                return nextLink;

            if (p.Locator != null && p.Locator.ToString().Contains("{0}"))
                return new Clickable(p.Locator.FillByTemplate(shortName));
            throw JDISettings.Exception(p.CantChooseElementMsg("Next", shortName, "nextAction"));
        };

        public By NextLocator;

        public Func<Pagination, int, Clickable> PageAction = (p, index) =>
        {
            const string shortName = "page";
            if (p.Locator != null && p.Locator.ToString().Contains("{0}"))
                return new Clickable(p.Locator.FillByTemplate(index));

            var pageLink = p.GetClickable(shortName);
            if (pageLink != null)
                return pageLink;
            throw JDISettings.Exception(p.CantChooseElementMsg(index.ToString(), shortName, "pageAction"));
        };

        public Func<Pagination, Clickable> PreviousAction = p =>
        {
            const string shortName = "prev";
            if (p.PreviousLocator != null)
                return new Clickable(p.PreviousLocator);

            var prevLink = p.GetClickable(shortName);
            if (prevLink != null)
                return prevLink;

            if (p.Locator != null && p.Locator.ToString().Contains("{0}"))
                return new Clickable(p.Locator.FillByTemplate(shortName));
            throw JDISettings.Exception(p.CantChooseElementMsg("Previous", shortName, "previousAction"));
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
            Invoker.DoJAction("Choose Next page", p => ((Pagination) p).NextAction(this).Click());
        }

        public void Previous()
        {
            Invoker.DoJAction("Choose Previous page", p => ((Pagination) p).PreviousAction(this).Click());
        }

        public void First()
        {
            Invoker.DoJAction("Choose First page", p => ((Pagination) p).FirstAction(this).Click());
        }

        public void Last()
        {
            Invoker.DoJAction("Choose Last page", p => ((Pagination) p).LastAction(this).Click());
        }

        public void SelectPage(int index)
        {
            Invoker.DoJAction($"Choose '{index}' page", p => ((Pagination) p).PageAction(this, index).Click());
        }

        private Clickable GetClickable(string name)
        {
            var fields = this.GetFields(typeof(IClickable));
            var result = fields.FirstOrDefault(field =>
                field.GetElementName().ToLower().Contains(name.ToLower()));
            return (Clickable) result?.GetValue(this);
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