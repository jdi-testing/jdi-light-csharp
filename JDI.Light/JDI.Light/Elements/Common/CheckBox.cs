﻿using System;
using JDI.Light.Elements.Base;
using JDI.Light.Interfaces.Common;
using OpenQA.Selenium;

namespace JDI.Light.Elements.Common
{
    public class CheckBox : Clickable, ICheckBox
    {
        public Action<CheckBox> CheckAction = el =>
        {
            if (!el.IsCheckedAction(el))
                el.Click();
            if (!el.IsCheckedAction(el))
                throw JDI.Assert.Exception("Can't check element. Verify locator for click or isCheckedAction");
        };

        public Func<CheckBox, bool> IsCheckedAction =
            el => el.IsSelected(el) || el.IsCheckedByAttribute(el);

        public Func<UIElement, bool> IsCheckedByAttribute =
            el => el.FindImmediately(() => el.WebElement.GetAttribute("checked") != null, false);

        public Func<UIElement, bool> IsSelected =
            el => el.FindImmediately(() => el.WebElement.Selected, false);

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
            Invoker.DoAction("Check Checkbox", () => CheckAction(this));
        }

        public void Uncheck()
        {
            Invoker.DoAction("Uncheck Checkbox", UncheckAction);
        }

        public bool IsChecked()
        {
            return Invoker.DoActionWithResult("IsChecked",
                () => IsCheckedAction(this),
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

        protected void UncheckAction()
        {
            if (IsCheckedAction(this))
                Click();
            if (IsCheckedAction(this))
                throw JDI.Assert.Exception("Can't uncheck element. Verify locator for click or isCheckedAction");
        }
    }
}