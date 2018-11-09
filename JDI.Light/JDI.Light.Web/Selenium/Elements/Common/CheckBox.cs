using System;
using JDI.Core.Interfaces.Common;
using JDI.Core.Settings;
using JDI.Web.Selenium.Base;
using JDI.Web.Selenium.Elements.Base;
using OpenQA.Selenium;

namespace JDI.Web.Selenium.Elements.Common
{
    public class CheckBox : Clickable, ICheckBox
    {
        public Action<CheckBox> CheckAction = el =>
        {
            if (!el.IsCheckedAction(el))
                el.ClickAction(el);
            if (!el.IsCheckedAction(el))
                throw JDISettings.Exception("Can't check element. Verify locator for click or isCheckedAction");
        };

        protected Func<WebBaseElement, string> GetValueFunc = el => ((CheckBox) el).IsChecked() + "";

        public Func<CheckBox, bool> IsCheckedAction =
            el => el.IsSelected(el) || el.IsCheckedByAttribute(el);

        public Func<WebBaseElement, bool> IsCheckedByAttribute =
            el => el.WebAvatar.FindImmediately(() => el.WebElement.GetAttribute("checked") != null, false);

        public Func<WebBaseElement, bool> IsSelected =
            el => el.WebAvatar.FindImmediately(() => el.WebElement.Selected, false);

        protected Action<WebBaseElement, string> SetValueAction = (el, value) =>
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
                    throw JDISettings.Exception(
                        $"SetValue not specified correctly {value}, expected: 'true','false','0','1','check','uncheck'");
            }
        };

        public CheckBox() : this(null)
        {
        }

        public CheckBox(By byLocator = null, IWebElement webElement = null, WebBaseElement element = null)
            : base(byLocator, webElement, element)
        {
        }

        public void Check()
        {
            Actions.Check(el => CheckAction(this));
        }

        public void Uncheck()
        {
            Actions.Uncheck(UncheckAction);
        }

        public bool IsChecked()
        {
            return Actions.IsChecked(el => IsCheckedAction(this));
        }

        public string Value
        {
            get => Actions.GetValue(GetValueFunc);
            set => Actions.SetValue(value, SetValueAction);
        }

        public string GetValue()
        {
            return Value;
        }

        protected void UncheckAction(WebBaseElement el)
        {
            if (IsCheckedAction((CheckBox) el))
                ClickAction(el);
            if (IsCheckedAction((CheckBox) el))
                throw JDISettings.Exception("Can't uncheck element. Verify locator for click or isCheckedAction");
        }
    }
}