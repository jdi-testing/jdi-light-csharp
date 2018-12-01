using System;
using JDI.Light.Interfaces.Base;
using OpenQA.Selenium;

namespace JDI.Light.Elements.Base
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

        public SelectableElement(By byLocator = null)
            : base(byLocator)
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