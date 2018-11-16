using System;
using System.Collections.Generic;
using System.Linq;
using JDI.Core.Interfaces.Complex;
using JDI.Core.Selenium.Base;
using JDI.Core.Settings;
using OpenQA.Selenium;

namespace JDI.Core.Selenium.Elements.Complex
{
    public class Selector : Selector<IConvertible>, ISelector
    {
        public Selector()
        {
        }

        public Selector(By optionsNamesLocatorTemplate, List<IWebElement> webElements = null,
            WebBaseElement element = null)
            : base(optionsNamesLocatorTemplate, webElements, element)
        {
        }

        public Selector(By optionsNamesLocatorTemplate, By allOptionsNamesLocator, List<IWebElement> webElements = null,
            WebBaseElement element = null)
            : base(optionsNamesLocatorTemplate, allOptionsNamesLocator, webElements, element)
        {
        }
    }

    public class Selector<TEnum> : BaseSelector<TEnum>, ISelector<TEnum>
        where TEnum : IConvertible
    {
        public Func<Selector<TEnum>, string> SelectedAction = s => s.Selected(s.Elements);

        public Func<Selector<TEnum>, int> SelectedIndexAction = s => s.SelectedIndex(s.Elements);

        protected Selector() : this(null)
        {
        }

        public Selector(By optionsNamesLocatorTemplate, List<IWebElement> webElements = null,
            WebBaseElement element = null) :
            base(optionsNamesLocatorTemplate, webElements, element)
        {
            // TODO: need to initialize in each constructor ??? (exclude to separate method)
            SelectedNameAction = (s, name) => ((Selector<TEnum>) s).SelectedAction(this).Equals(name);
            GetValueAction = s => ((Selector<TEnum>) s).Selected();
        }

        public Selector(By optionsNamesLocatorTemplate, By allOptionsNamesLocator, List<IWebElement> webElements = null,
            WebBaseElement element = null)
            : base(optionsNamesLocatorTemplate, allOptionsNamesLocator, webElements, element)
        {
            SelectedNameAction = (s, name) => ((Selector<TEnum>) s).SelectedAction(this).Equals(name);
            SelectedNumAction = (s, num) => ((Selector<TEnum>) s).SelectedIndexAction(this) == num;
            GetValueAction = s => ((Selector<TEnum>) s).Selected();
        }

        public void Select(string name)
        {
            Actions.Select(name, (s, n) => SelectNameAction(this, name));
        }

        public void Select(TEnum enumType)
        {
            Select(enumType.ToString());
        }

        public void Select(int num)
        {
            Actions.Select(num, (s, n) => SelectNumAction(this, num));
        }

        public string Selected()
        {
            return Actions.Selected(s => SelectedAction(this));
        }

        public int SelectedIndex()
        {
            return Actions.SelectedIndex(s => SelectedIndexAction(this));
        }

        public string GetValue()
        {
            return Value;
        }

        public string Selected(IList<IWebElement> els)
        {
            var element = els.FirstOrDefault(el => SelectedElementAction(this, el));
            if (element == null)
                throw JDISettings.Exception(
                    "No elements selected. Override getSelectedAction or place locator to <select> tag");
            return element.Text;
        }

        private int SelectedIndex(IList<IWebElement> els)
        {
            var num = els.ToList().FindIndex(el => SelectedElementAction(this, el)) + 1;
            if (num == 0)
                throw JDISettings.Exception(
                    "No elements selected. Override getSelectedAction or place locator to <select> tag");
            return num;
        }
    }
}