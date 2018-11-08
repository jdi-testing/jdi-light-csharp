using JDI.Web.Selenium.Base;
using OpenQA.Selenium;

namespace JDI.Web.Selenium.Elements.Composite
{
    public class Alert : Popup
    {
        private IAlert alert;
        private IAlert GetAlert()
        {
            if (alert == null)
                alert = new WebBaseElement().WebDriver.SwitchTo().Alert();
            return alert;
        }

        protected void OkAction()
        {
            alert.Accept();
        }

        protected void cancelAction()
        {
            alert.Dismiss();
        }

        protected void closeAction()
        {
            alert.Dismiss();
        }

        protected string GetTextAction()
        {
            return alert.Text;
        }
    }
}
