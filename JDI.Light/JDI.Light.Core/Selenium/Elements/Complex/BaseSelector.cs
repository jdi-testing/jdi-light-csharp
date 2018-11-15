using System;
using System.Collections.Generic;
using System.Linq;
using JDI.Core.Extensions;
using JDI.Core.Interfaces.Base;
using JDI.Core.Selenium.Base;
using JDI.Core.Selenium.DriverFactory;
using JDI.Core.Selenium.Elements.Base;
using JDI.Core.Settings;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace JDI.Core.Selenium.Elements.Complex
{
    public abstract class BaseSelector<TEnum> : WebBaseElement, IVisible
        where TEnum : IConvertible
    {
        public Func<BaseSelector<TEnum>, bool> DisplayedAction = s =>
        {
            var els = s.WebAvatar.FindImmediately(() => s.Elements, null);
            return els != null && els.Any() && els[0].Displayed;
        };

        public Func<BaseSelector<TEnum>, string, bool> DisplayedNameAction = (s, name) =>
        {
            var el = s.GetWebElement(name);
            return el != null && el.Displayed;
        };

        public Func<BaseSelector<TEnum>, int, bool> DisplayedNumAction =
            (s, num) => s.DisplayedInList(s.Elements, num);

        public Func<BaseSelector<TEnum>, List<string>> GetOptionsAction =
            s => s.Elements.Select(el => el.Text).ToList();

        public Func<BaseSelector<TEnum>, string> GetValueAction;

        public Func<BaseSelector<TEnum>, string, IWebElement> GetWebElementFunc = (s, name) =>
        {
            if (!s.HasLocator)
                throw JDISettings.Exception("Element has no locators");
            return s.Locator.ToString().Contains("{0}")
                ? new WebElement(s.Locator.FillByTemplate(name))
                {
                    WebAvatar = {DriverName = s.WebAvatar.DriverName},
                    Parent = s.Parent
                }.WebElement
                : s.Elements.FirstOrDefault(el => el.Text.Equals(name));
        };

        protected bool IsSelector;

        public Func<BaseSelector<TEnum>, IWebElement, bool> SelectedElementAction = (s, el) =>
        {
            if (s.IsSelector)
                return el.Selected;
            var attr = el.GetAttribute("checked");
            return attr != null && attr.Equals("true");
        };

        public Func<BaseSelector<TEnum>, string, bool> SelectedNameAction;
        public Func<BaseSelector<TEnum>, int, bool> SelectedNumAction;

        public Action<BaseSelector<TEnum>, string> SelectNameAction = (s, name) =>
        {
            if (!s.HasLocator && s.AllLabels == null)
                throw JDISettings.Exception(
                    $"Can't find option '{name}'. No optionsNamesLocator and _allLabelsLocator found");
            if (s.Locator.ToString().Contains("{0}"))
            {
                new Clickable(s.Locator.FillByTemplate(name), element: s).Click();
                return;
            }

            if (s.AllLabels != null)
            {
                s.SelectFromList(s.AllLabels.WebElements, name);
                return;
            }

            var elements = s.WebAvatar.SearchAll().WebElements;
            var selector = GetSelectElement(elements);
            if (selector != null)
                if (selector.Options.Any())
                {
                    selector.SelectByText(name);
                    return;
                }
                else
                {
                    throw JDISettings.Exception(
                        $"<select> tag has no <option> tags. Please Clarify element locator ({s})");
                }

            if (elements.Count == 1 && elements[0].TagName.Equals("ul"))
                elements = elements[0].FindElements(By.TagName("li")).ToList();
            s.SelectFromList(elements, name);
        };

        public Action<BaseSelector<TEnum>, int> SelectNumAction = (s, num) =>
        {
            if (!s.HasLocator && s.AllLabels == null)
                throw JDISettings.Exception(
                    $"Can't find option '{num}'. No optionsNamesLocator and _allLabelsLocator found");
            if (s.AllLabels != null)
            {
                s.SelectFromList(s.AllLabels.WebElements, num);
                return;
            }

            if (s.Locator.ToString().Contains("{0}"))
            {
                new Clickable(s.Locator.FillByTemplate(num)).Click();
                return;
            }

            var elements = s.WebAvatar.SearchAll().WebElements;
            if (elements.Count == 1 && elements[0].TagName.Equals("select"))
                if (s.Selector.Options.Any())
                {
                    s.Selector.SelectByIndex(num - 1);
                    return;
                }
                else
                {
                    throw JDISettings.Exception(
                        $"<select> tag has no <option> tags. Please Clarify element locator ({s})");
                }

            if (elements.Count == 1 && elements[0].TagName.Equals("ul"))
                elements = elements[0].FindElements(By.TagName("li")).ToList();
            s.SelectFromList(elements, num);
        };

        public Func<BaseSelector<TEnum>, bool> WaitDisplayedAction = s =>
        {
            return s.Timer.Wait(() =>
            {
                var els = s.Elements;
                return els != null && els.Any() && els[0].Displayed;
            });
        };

        public Func<BaseSelector<TEnum>, bool> WaitVanishedAction =
            s => s.Timer.Wait(() => !s.DisplayedAction(s));

        protected BaseSelector(By optionsNamesLocator, List<IWebElement> webElements = null,
            WebBaseElement element = null) :
            base(optionsNamesLocator, webElements: webElements, element: element)
        {
        }

        protected BaseSelector(By optionsNamesLocator, By allLabelsLocator, List<IWebElement> webElements = null,
            WebBaseElement element = null)
            : base(optionsNamesLocator, webElements: webElements, element: element)
        {
            AllLabels = (TextList) new TextList().SetAvatar(WebAvatar, allLabelsLocator);
        }

        protected SelectElement Selector
        {
            get
            {
                IsSelector = true;
                return new SelectElement(WebElement);
            }
        }

        public TextList AllLabels { get; set; }

        public virtual Action<BaseSelector<TEnum>, string> SetValueAction { get; set; } =
            (s, value) => s.SelectNameAction(s, value);

        public string Value
        {
            get { return Actions.GetValue(el => GetValueAction(this)); }
            set { Actions.SetValue(value, (el, val) => SetValueAction(this, val)); }
        }

        public IList<string> Options => GetOptionsAction(this);
        public IList<string> Names => Options;
        public IList<string> Values => Options;

        public string OptionsAsText => Options.Print();

        public IList<IWebElement> Elements
        {
            get
            {
                if (!HasLocator && AllLabels == null)
                    throw JDISettings.Exception(
                        "Can't check is element displayed or not. No optionsNamesLocator and allLabelsLocator found");
                if (AllLabels != null)
                    return AllLabels.WebElements;
                if (Locator.ToString().Contains("{0}"))
                    throw JDISettings.Exception(
                        "Can't check is element displayed or not. Please specify allLabelsLocator or correct optionsNamesLocator (should not contain '{0}')");
                return GetElementsFromTag();
            }
        }

        public bool Displayed => Actions.IsDisplayed(s => DisplayedAction(this));

        public bool Hidden => Actions.IsDisplayed(s => !DisplayedAction(this));

        public void WaitDisplayed()
        {
            Actions.WaitDisplayed(s => WaitDisplayedAction(this));
        }

        public void WaitVanished()
        {
            Actions.WaitVanished(s => Timer.Wait(() => !DisplayedAction(this)));
        }

        private static SelectElement GetSelectElement(List<IWebElement> elements)
        {
            var selector = elements.Count == 1 ? elements[0] : null;
            if (selector == null
                && elements.Count(el => WebSettings.WebDriverFactory.ElementSearchCriteria(el)
                                        && el.TagName.Equals("select")) == 1)
                selector = elements
                    .First(el => WebSettings.WebDriverFactory.ElementSearchCriteria(el) && el.TagName.Equals("select"));
            return selector != null ? new SelectElement(selector) : null;
        }

        private void SelectFromList(IList<IWebElement> els, string name)
        {
            var element = els.FirstOrDefault(el => el.Text.Equals(name));
            if (element == null)
                throw JDISettings.Exception($"Can't find option '{name}'. Please fix _allLabelsLocator");
            element.Click();
        }

        private void SelectFromList(IList<IWebElement> els, int num)
        {
            if (num <= 0)
                throw JDISettings.Exception($"Can't get option with num '{num}'. num should be 1 or more");
            if (els == null)
                throw JDISettings.Exception($"Can't find option with num '{num}'. Please fix _allLabelsLocator");
            if (els.Count < num)
                throw JDISettings.Exception($"Can't find option with num '{num}'. Find only '{els.Count}' options");
            els[num - 1].Click();
        }

        public void WaitSelected(string name)
        {
            Actions.Selected(name, (el, n) => TimerExtensions.ForceDone(() => SelectedNameAction(this, n)));
        }

        public void WaitSelected(TEnum enumType)
        {
            WaitSelected(enumType.ToString());
        }

        public bool Selected(string name)
        {
            return Actions.Selected(name, (el, n) => SelectedNameAction(this, n));
        }

        public bool Selected(TEnum enumType)
        {
            return Selected(enumType.ToString());
        }

        public IList<IWebElement> GetElementsFromTag()
        {
            IList<IWebElement> elements;
            try
            {
                elements = WebAvatar.SearchAll().WebElements;
            }
            catch
            {
                return new List<IWebElement>();
            }

            if (elements.Count == 1)
                switch (elements[0].TagName)
                {
                    case "select":
                        return Selector.Options;
                    case "ul":
                        return elements[0].FindElements(By.TagName("li"));
                }
            return elements;
        }

        public IWebElement GetWebElement(string name)
        {
            return GetWebElementFunc(this, name);
        }

        private bool DisplayedInList(IList<IWebElement> els, int num)
        {
            if (num <= 0)
                throw JDISettings.Exception($"Can't get option with num '{num}'. num should be 1 or more");
            if (els == null)
                throw JDISettings.Exception($"Can't find option with num '{num}'. Please fix _allLabelsLocator");
            if (els.Count < num)
                throw JDISettings.Exception($"Can't find option with num '{num}'. Find '{els.Count}' options");
            return els[num - 1].Displayed;
        }
    }
}