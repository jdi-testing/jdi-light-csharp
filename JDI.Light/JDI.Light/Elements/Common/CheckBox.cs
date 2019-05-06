using System;
using JDI.Light.Elements.Base;
using JDI.Light.Interfaces.Common;
using OpenQA.Selenium;

namespace JDI.Light.Elements.Common
{
    public class CheckBox : UIElement, ICheckBox
    {
        private Func<UIElement, bool> _isCheckedFunc = e =>
        {
            var res = e.FindImmediately(() => e.WebElement.Selected
                                              || e.WebElement.GetAttribute("checked") != null, false);
            return res;
        };

        internal void SetIsCheckedFunc(Func<UIElement, bool> func)
        {
            _isCheckedFunc = func;
        }

        protected Action<CheckBox, bool, bool> SetValueAction = (cb, value, checkEnabled) =>
        {
            if (value)
            {
                cb.Check(checkEnabled);
            }
            else
            {
                cb.Uncheck(checkEnabled);
            }
        };

        public CheckBox() : this(null)
        {
        }

        public CheckBox(By byLocator = null)
            : base(byLocator)
        {
        }

        public void Check(bool checkEnabled = true)
        {
            CheckEnabled(checkEnabled);
            Invoker.DoActionWithWait("Check Checkbox", () => 
            {
                if (!_isCheckedFunc(this))
                    Click();
                if (!_isCheckedFunc(this))
                    throw Jdi.Assert.Exception("Can't check element. Verify locator for click or isCheckedAction");
            });
        }

        public void Uncheck(bool checkEnabled = true)
        {
            CheckEnabled(checkEnabled);
            Invoker.DoActionWithWait("Uncheck Checkbox", () => {
                if (_isCheckedFunc(this))
                    Click();
                if (_isCheckedFunc(this))
                    throw Jdi.Assert.Exception("Can't uncheck element. Verify locator for click or isCheckedAction");
            });
        }

        public bool IsChecked => Invoker.DoActionWithResult("IsChecked",
                () => _isCheckedFunc(this),
                result => "Checkbox is " + (result ? "checked" : "unchecked"));
        
        public bool Value
        {
            get => Invoker.DoActionWithResult("Get value", () => _isCheckedFunc(this));
            set => Invoker.DoAction("Set value", () => SetValueAction(this, value, true));
        }

        public bool GetValue()
        {
            return Value;
        }
    }
}