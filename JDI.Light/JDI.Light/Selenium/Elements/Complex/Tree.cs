using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using JDI.Light.Extensions;
using JDI.Light.Selenium.DriverFactory;
using JDI.Light.Selenium.Elements.Base;
using JDI.Light.Utils;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace JDI.Light.Selenium.Elements.Complex
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
                        throw JDI.Assert.Exception($"Can't select element by path '{names.FormattedJoin(m.Separator)}'");
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
            Actions.Select(names.FormattedJoin(), (w, n) => ChooseItemAction(this, names));
        }
    }
}