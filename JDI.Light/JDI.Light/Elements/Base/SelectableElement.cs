using System;
using JDI.Light.Interfaces.Base;

namespace JDI.Light.Elements.Base
{
    public class SelectableElement : Clickable, ISelectable<bool>
    {
        protected Func<UIElement, string> GetValueFunc = el
            => ((SelectableElement) el).Selected + "";
        
        protected Func<SelectableElement, bool> SelectedAction = s => s.WebElement.Selected;

        protected Action<UIElement, bool> SetValueAction = (el, value)
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
            Invoker.DoActionWithWait($"Select '{Name}'", Click);
        }

        public bool Selected => Invoker.DoActionWithResult("Is Selected", () => SelectedAction(this));

        public bool Value
        {
            get => Invoker.DoActionWithResult("Get value", () => SelectedAction(this));
            set => Invoker.DoActionWithWait("Get value", () => SetValueAction(this, value));
        }

        public bool GetValue()
        {
            return Value;
        }
    }
}