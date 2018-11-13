using JDI.Core.Selenium.Base;
using OpenQA.Selenium;

namespace JDI.Core.Selenium.Elements.Composite
{
    public class Alert : Popup
    {
        private IAlert _alert;

        private IAlert GetAlert()
        {
            return _alert ?? (_alert = new WebBaseElement().WebDriver.SwitchTo().Alert());
        }

        protected void OkAction()
        {
            _alert.Accept();
        }

        protected void CancelAction()
        {
            _alert.Dismiss();
        }

        protected void CloseAction()
        {
            _alert.Dismiss();
        }

        protected new string GetTextAction()
        {
            return _alert.Text;
        }
    }
}