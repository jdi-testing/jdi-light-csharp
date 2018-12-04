using System;
using JDI.Light.Elements.Base;
using JDI.Light.Extensions;
using JDI.Light.Interfaces.Complex;
using OpenQA.Selenium;

namespace JDI.Light.Elements.Complex
{
    public class DropList : DropList<IConvertible>, IDropList
    {
        public DropList()
        {
        }

        public DropList(By valueLocator) : base(valueLocator)
        {
        }

        public DropList(By valueLocator, By optionsNamesLocator, By allOptionsNamesLocator = null)
            : base(valueLocator, optionsNamesLocator, allOptionsNamesLocator)
        {
        }
    }

    public class DropList<TEnum> : MultiSelector<TEnum>, IDropList<TEnum>
        where TEnum : IConvertible
    {
        protected Action<DropList<TEnum>, string> ExpandNameAction = (d, name) =>
        {
            if (!d.DisplayedNameAction(d, name))
                d.Button.Click();
        };

        protected Action<DropList<TEnum>, int> ExpandNumAction = (d, index) =>
        {
            if (!d.DisplayedNumAction(d, index))
                d.Button.Click();
        };

        protected Func<DropList<TEnum>, string> GetTextAction =
            d => d.WebElement.GetAttribute("value");

        public DropList() : this(null)
        {
        }

        public DropList(By valueLocator) : base(valueLocator)
        {
            InitActions();
        }

        public DropList(By valueLocator, By optionsNamesLocator, By allOptionsNamesLocator = null)
            : base(optionsNamesLocator, allOptionsNamesLocator)
        {
            InitActions();
            Button = new Clickable(valueLocator);
        }

        protected Clickable Button { get; set; }

        public new IWebElement WebElement => new UIElement(Locator)
        {
            DriverName = DriverName,
            Parent = Parent
        }.WebElement;
        
        public new void WaitDisplayed()
        {
            Button.WaitDisplayed();
        }

        public new void WaitVanished()
        {
            Button.WaitVanished();
        }

        public new void SetAttribute(string attributeName, string value)
        {
            Button.SetAttribute(attributeName, value);
        }

        public string GetText => Invoker.DoActionWithResult("Get text", () => GetTextAction(this));

        Func<string> TextAction() => () => GetTextAction(this);

        public string WaitText(string text)
        {
            return Invoker.DoActionWithResult($"Wait text contains '{text}'",
                () => TextAction().GetByCondition(t => t.Contains(text)));
        }

        public string WaitMatchText(string regEx)
        {
            return Invoker.DoActionWithResult($"Wait text match regex '{regEx}'",
                () => TextAction().GetByCondition(t => t.Matches(regEx)));
        }

        public new string GetAttribute(string name)
        {
            return Button.GetAttribute(name);
        }

        public new void WaitAttribute(string name, string value)
        {
            Button.WaitAttribute(name, value);
        }

        private void InitActions()
        {
            SelectListNamesAction = (d, names) =>
            {
                if (names == null || names.Count == 0)
                    return;
                if (Button != null)
                {
                    ExpandNameAction(this, names[0]);
                    SelectListNamesAction(this, names);
                }
                else
                {
                    names.ForEach(name => Selector.SelectByText(name));
                }
            };
            SelectListIndexesAction = (d, indexes) =>
            {
                if (indexes == null || indexes.Count == 0)
                    return;
                if (Button != null)
                {
                    ExpandNumAction(this, indexes[0]);
                    SelectListIndexesAction(this, indexes);
                }
                else
                {
                    indexes.ForEach(index => Selector.SelectByIndex(index));
                }
            };
            ClearAction = d =>
            {
                if (Button != null)
                    ExpandNumAction(this, 1);
                ClearAction(this);
            };
            GetValueAction = d => GetTextAction(this);
        }
    }
}