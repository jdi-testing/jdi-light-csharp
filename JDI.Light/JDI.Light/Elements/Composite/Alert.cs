using JDI.Light.Elements.Base;
using OpenQA.Selenium;

namespace JDI.Light.Elements.Composite
{
    public class Alert : UIElement
    {
        public Alert() : base(null)
        {

        }

        private IAlert alert;
        private IAlert GetAlert()
        {
            if (alert == null)
            {
                alert = WebDriver.SwitchTo().Alert();
            }
            return alert;
        }

        public void OkAction()
        {
            GetAlert().Accept();
        }

        public void CancelAction()
        {
            GetAlert().Dismiss();
        }

        public void CloseAction()
        {
            GetAlert().Dismiss();
        }

        public string GetTextAction()
        {
            return GetAlert().Text;
        }
    }
}