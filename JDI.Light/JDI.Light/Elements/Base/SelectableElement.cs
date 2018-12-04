using System;
using JDI.Light.Interfaces.Base;
using OpenQA.Selenium;

namespace JDI.Light.Elements.Base
{
    public class SelectableElement : ClickableText, ISelect
    {
        protected Func<UIElement, string> GetValueFunc = el
            => ((SelectableElement) el).Selected + "";
        
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
            Invoker.DoAction($"Select '{Name}'", Click);
        }

        public bool Selected => Invoker.DoActionWithResult("Is Selected", () => SelectedAction(this));

        public new string Value
        {
            get => Invoker.DoActionWithResult("Get value", Selected.ToString);
            set => Invoker.DoAction("Get value", () => SetValueAction(this, value));
        }
    }
}