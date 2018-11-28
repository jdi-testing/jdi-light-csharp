using System;
using JDI.Light.Interfaces.Complex;
using JDI.Light.Selenium.Elements.Base;
using JDI.Light.Selenium.Elements.Common;
using JDI.Light.Settings;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace JDI.Light.Selenium.Elements.Complex
{
    public class Dropdown : Dropdown<IConvertible>, IDropDown
    {
        public Dropdown()
        {
        }

        public Dropdown(By selectLocator = null) : base(selectLocator, null)
        {
        }

        public Dropdown(By selectLocator, By optionsNamesLocator, By allOptionsNamesLocator = null)
            : base(selectLocator, optionsNamesLocator, allOptionsNamesLocator)
        {
        }
    }

    public class Dropdown<TEnum> : Selector<TEnum>, IDropDown<TEnum>
        where TEnum : IConvertible
    {
        public Action<Dropdown<TEnum>, string> ExpandNameAction = (d, name) =>
        {
            if (!d.Element.Displayed) return;
            d.SetWaitTimeout(0);
            if (!d.DisplayedNameAction(d, name)) d.Element.Click();
            d.SetWaitTimeout(JDI.Timeouts.WaitElementSec);
        };

        public Action<Dropdown<TEnum>, int> ExpandNumAction = (d, index) =>
        {
            if (!d.DisplayedNumAction(d, index))
                d.Element.Click();
        };

        public Dropdown() : this(null)
        {
        }

        public Dropdown(By selectLocator) : base(selectLocator)
        {
        }

        public Dropdown(By selectLocator, By optionsNamesLocator, By allOptionsNamesLocator = null)
            : base(optionsNamesLocator, allOptionsNamesLocator)
        {
            SelectNameAction = (s, name) =>
            {
                var selector = new Selector(optionsNamesLocator, allOptionsNamesLocator);
                if (Element != null)
                {
                    ExpandNameAction(this, name);
                    selector.SelectNameAction(selector, name);
                }
                else
                {
                    new SelectElement(WebElement).SelectByText(name);
                }
            };
            SelectNumAction = (s, index) =>
            {
                var selector = new Selector(optionsNamesLocator, allOptionsNamesLocator);
                if (Element != null)
                {
                    ExpandNumAction(this, index);
                    selector.SelectNumAction(selector, index);
                }
                else
                {
                    new SelectElement(WebElement).SelectByIndex(index);
                }
            };
            GetValueAction = b => GetTextAction(this);
            SelectedAction = s => GetTextAction(this);
            GetOptionsAction = d =>
            {
                var selector = new Selector(optionsNamesLocator, allOptionsNamesLocator);
                var isExpanded = DisplayedNumAction(this, 1);
                if (!isExpanded) Element.Click();
                var result = selector.GetOptionsAction(selector);
                if (!isExpanded) Element.Click();
                return result;
            };
            Element = new Label(selectLocator);
        }

        protected Label Element { get; set; }

        public virtual Action<Dropdown<TEnum>> ClickAction { get; set; } = d => d.Element.Click();
        public virtual Func<Dropdown<TEnum>, string> GetTextAction { get; set; } = d => d.Element.GetText;

        public new IWebElement WebElement => new UIElement(Locator).WebElement;
        
        public void Expand()
        {
            if (!DisplayedNumAction(this, 1)) Element.Click();
        }

        public void Close()
        {
            if (DisplayedNumAction(this, 1)) Element.Click();
        }

        public void Click()
        {
            Actions.Click(d => ClickAction(this));
        }

        public string GetText => Actions.GetText(d => GetTextAction(this));

        public string WaitText(string text)
        {
            return Actions.WaitText(text, d => GetTextAction(this));
        }

        public string WaitMatchText(string regEx)
        {
            return Actions.WaitMatchText(regEx, d => GetTextAction(this));
        }

        public new void SetAttribute(string attributeName, string value)
        {
            Element.SetAttribute(attributeName, value);
        }

        public new string GetAttribute(string name)
        {
            return Element.GetAttribute(name);
        }

        public new void WaitAttribute(string name, string value)
        {
            Element.WaitAttribute(name, value);
        }

        public void SetUp(By root, By value, By list, By expand, By elementByName)
        {
            if (root != null)
            {
                var el = new UIElement(root)
                {
                    DriverName = DriverName,
                    Parent = Parent
                };
                Parent = el;
            }

            if (value != null)
            {
                Element = new Label(value);
            }

            if (list != null)
            {
                AllLabels = new TextList(list);
            }
        }
    }
}