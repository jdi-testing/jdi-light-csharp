using System;
using JDI.Light.Elements.Base;
using JDI.Light.Interfaces.Common;
using OpenQA.Selenium;

namespace JDI.Light.Elements.Common
{
    public class CheckBox : Clickable, ICheckBox
    {
        protected Action<UIElement, string> SetValueAction = (el, value) =>
        {
            switch (value.ToLower())
            {
                case "true":
                case "1":
                case "check":
                    ((CheckBox) el).Check();
                    break;
                case "false":
                case "0":
                case "uncheck":
                    ((CheckBox) el).Uncheck();
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
            Invoker.DoAction("Check Checkbox", () => 
            {
                if (!IsChecked())
                    Click();
                if (!IsChecked())
                    throw JDI.Assert.Exception("Can't check element. Verify locator for click or isCheckedAction");
            });
        }

        public void Uncheck()
        {
            Invoker.DoAction("Uncheck Checkbox", () => {
                if (IsChecked())
                    Click();
                if (IsChecked())
                    throw JDI.Assert.Exception("Can't uncheck element. Verify locator for click or isCheckedAction");
            });
        }

        public bool IsChecked()
        {
            return Invoker.DoActionWithResult("IsChecked",
                () =>
                {
                    return FindImmediately(() => WebElement.Selected || WebElement.GetAttribute("checked") != null, false);
                },
                result => "Checkbox is " + (result ? "checked" : "unchecked"));
        }

        public string Value
        {
            get => Invoker.DoActionWithResult("Get value", IsChecked().ToString);
            set => Invoker.DoAction("Get value", () => SetValueAction(this, value));
        }

        public string GetValue()
        {
            return Value;
        }
    }
}