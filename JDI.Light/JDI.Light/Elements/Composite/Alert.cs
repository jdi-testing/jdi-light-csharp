using JDI.Light.Elements.Base;
using OpenQA.Selenium;

namespace JDI.Light.Elements.Composite
{
    public class Alert : UIElement
    {
        public Alert() : base(null)
        {
        }

        private IAlert _alert;
        private IAlert GetAlert()
        {
            return _alert ?? (_alert = WebDriver.SwitchTo().Alert());
        }

        public void Ok()
        {
            GetAlert().Accept();
        }

        public void Cancel()
        {
            GetAlert().Dismiss();
        }

        public new void SendKeys(string keysToSend)
        {
            GetAlert().SendKeys(keysToSend);
        }

        public string GetText()
        {
            return GetAlert().Text;
        }
    }
}