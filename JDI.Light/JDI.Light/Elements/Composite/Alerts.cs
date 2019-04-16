using JDI.Light.Elements.Base;
using OpenQA.Selenium;

namespace JDI.Light.Elements.Composite
{
    public class Alerts : UIElement
    {
        public Alerts() : base(null)
        {
        }

        private IAlert _alert;
        private IAlert GetAlert()
        {
            return _alert ?? (_alert = WebDriver.SwitchTo().Alert());
        }

        public void AcceptAlert()
        {
            GetAlert().Accept();
        }

        public void DismissAlert()
        {
            GetAlert().Dismiss();
        }

        public void InputAndAcceptAlert(string keysToSend)
        {
            GetAlert().SendKeys(keysToSend);
            GetAlert().Accept();
        }

        public string GetAlertText()
        {
            return GetAlert().Text;
        }
    }
}