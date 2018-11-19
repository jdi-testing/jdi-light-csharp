using System;
using JDI.Core.Interfaces.Base;
using JDI.Core.Selenium.Base;
using OpenQA.Selenium;

namespace JDI.Core.Selenium.Elements.Base
{
    public class SelectableElement : ClickableText, ISelect
    {
        protected Func<UIElement, string> GetValueFunc = el
            => ((SelectableElement) el).Selected + "";

        protected Action<SelectableElement> SelectAction = s => s.Click();

        protected Func<SelectableElement, bool> SelectedAction = s => s.WebElement.Selected;

        protected Action<UIElement, string> SetValueAction = (el, value)
            => ((SelectableElement) el).Select();

        public SelectableElement() : this(null)
        {
        }

        public SelectableElement(By byLocator = null, IWebElement webElement = null, UIElement element = null)
            : base(byLocator, webElement, element)
        {
        }

        public void Select()
        {
            Actions.Select(Name, (w, n) => ClickAction(this));
        }

        public bool Selected => Actions.Selected(w => SelectedAction(this));

        public new string Value
        {
            get => Actions.GetValue(GetValueFunc);
            set => Actions.SetValue(value, SetValueAction);
        }
    }
}