using System;
using JDI.Light.Elements.Base;
using JDI.Light.Interfaces.Common;
using OpenQA.Selenium;

namespace JDI.Light.Elements.Common
{
    public class CheckBox : Clickable, ICheckBox
    {
        public Func<UIElement, bool> IsCheckedFunc = e =>
        {
            var res = e.FindImmediately(() => e.WebElement.Selected
                                              || e.WebElement.GetAttribute("checked") != null, false);
            return res;
        };

        protected Action<CheckBox, string> SetValueAction = (cb, value) =>
        {
            switch (value.ToLower())
            {
                case "true":
                case "1":
                case "check":
                    cb.Check();
                    break;
                case "false":
                case "0":
                case "uncheck":
                    cb.Uncheck();
                    break;
                default:
                    throw JDI.Assert.Exception(
                        $"SetValue not specified correctly {value}, expected: 'true','false','0','1','check','uncheck'");
            }
        };

        public CheckBox() : this(null)
        {
        }

        public CheckBox(By byLocator = null)
            : base(byLocator)
        {
        }

        public void Check()
        {
            Invoker.DoActionWithWait("Check Checkbox", () => 
            {
                if (!IsCheckedFunc(this))
                    Click();
                if (!IsCheckedFunc(this))
                    throw JDI.Assert.Exception("Can't check element. Verify locator for click or isCheckedAction");
            });
        }

        public void Uncheck()
        {
            Invoker.DoActionWithWait("Uncheck Checkbox", () => {
                if (IsCheckedFunc(this))
                    Click();
                if (IsCheckedFunc(this))
                    throw JDI.Assert.Exception("Can't uncheck element. Verify locator for click or isCheckedAction");
            });
        }

        public bool IsChecked()
        {
            return Invoker.DoActionWithResult("IsChecked",
                () => IsCheckedFunc(this),
                result => "Checkbox is " + (result ? "checked" : "unchecked"));
        }

        public string Value
        {
            get => Invoker.DoActionWithResult("Get value", IsCheckedFunc(this).ToString);
            set => Invoker.DoAction("Set value", () => SetValueAction(this, value));
        }

        public string GetValue()
        {
            return Value;
        }
    }
}