using System;
using System.Collections.Generic;
using System.Linq;
using JDI.Light.Interfaces.Complex;
using OpenQA.Selenium;

namespace JDI.Light.Elements.Complex
{
    public class Selector : Selector<IConvertible>, ISelector
    {
        public Selector()
        {
        }

        public Selector(By optionsNamesLocatorTemplate)
            : base(optionsNamesLocatorTemplate)
        {
        }

        public Selector(By optionsNamesLocatorTemplate, By allOptionsNamesLocator)
            : base(optionsNamesLocatorTemplate, allOptionsNamesLocator)
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

        public Selector(By optionsNamesLocatorTemplate) :
            base(optionsNamesLocatorTemplate)
        {
            // TODO: need to initialize in each constructor ??? (exclude to separate method)
            SelectedNameAction = (s, name) => ((Selector<TEnum>) s).SelectedAction(this).Equals(name);
            GetValueAction = s => ((Selector<TEnum>) s).Selected();
        }

        public Selector(By optionsNamesLocatorTemplate, By allOptionsNamesLocator)
            : base(optionsNamesLocatorTemplate, allOptionsNamesLocator)
        {
            SelectedNameAction = (s, name) => ((Selector<TEnum>) s).SelectedAction(this).Equals(name);
            SelectedNumAction = (s, num) => ((Selector<TEnum>) s).SelectedIndexAction(this) == num;
            GetValueAction = s => ((Selector<TEnum>) s).Selected();
        }

        public void Select(string name)
        {
            Invoker.DoAction($"Select '{name}'", el => SelectNameAction(this, name));
        }

        public void Select(TEnum enumType)
        {
            Select(enumType.ToString());
        }

        public void Select(int num)
        {
            Invoker.DoAction($"Select '{num}'", el => SelectNumAction(this, num));
        }

        public string Selected()
        {
            return Invoker.DoActionWithResult("Get Selected element name", s => SelectedAction(this));
        }

        public int SelectedIndex()
        {
            return Invoker.DoActionWithResult("Get Selected element index", s => SelectedIndexAction(this));
        }

        public string GetValue()
        {
            return Value;
        }

        public string Selected(IList<IWebElement> els)
        {
            var element = els.FirstOrDefault(el => SelectedElementAction(this, el));
            if (element == null)
                throw JDI.Assert.Exception(
                    "No elements selected. Override getSelectedAction or place locator to <select> tag");
            return element.Text;
        }

        private int SelectedIndex(IList<IWebElement> els)
        {
            var num = els.ToList().FindIndex(el => SelectedElementAction(this, el)) + 1;
            if (num == 0)
                throw JDI.Assert.Exception(
                    "No elements selected. Override getSelectedAction or place locator to <select> tag");
            return num;
        }
    }
}