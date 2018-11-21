using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using JDI.Core.Extensions;
using JDI.Core.Selenium.Base;
using JDI.Core.Selenium.DriverFactory;
using JDI.Core.Settings;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace JDI.Core.Selenium.Elements.Complex
{
    public class Tree : Selector
    {
        protected Action<Tree, string[]> ChooseItemAction =
            (m, names) =>
            {
                var nodes = m.SplitToList(names, m.Separator);
                ISearchContext ctx = m.WebDriver;
                nodes.ForEach(node =>
                {
                    var elements = ctx.FindElements(m.Locator.FillByTemplate(node));
                    if (elements == null || elements.Count != 0)
                        throw JDISettings.Exception($"Can't select element by path '{names.Print(m.Separator)}'");
                    var element = elements.First();
                    element.Click();
                    ctx = element;
                });
            };

        protected Action<UIElement, IWebElement> HoverAction = (m, el) =>
        {
            var action = new Actions(m.WebDriver);
            action.MoveToElement(el).ClickAndHold().Build().Perform();
        };

        public string Separator = "\\|";

        public Tree()
        {
            SelectNameAction = (m, name) => ChooseItemAction(this, new[] {name});
        }

        public Tree(By optionsNamesLocatorTemplate)
            : base(optionsNamesLocatorTemplate)
        {
        }

        public Tree(By optionsNamesLocatorTemplate, By allOptionsNamesLocator)
            : base(optionsNamesLocatorTemplate, allOptionsNamesLocator)
        {
        }

        public Tree UseSeparator(string separator)
        {
            Separator = separator;
            return this;
        }
        
        private IList<string> SplitToList(string[] str, string separator)
        {
            return (str.Length == 1
                ? Regex.Split(str[0], separator)
                : str).ToList();
        }

        public void Select(params string[] names)
        {
            Actions.Select(names.Print(), (w, n) => ChooseItemAction(this, names));
        }
    }
}