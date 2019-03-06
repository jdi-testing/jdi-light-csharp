using JDI.Light.Attributes;
using JDI.Light.Elements.Common;
using JDI.Light.Elements.Composite;
using JDI.Light.Interfaces.Common;

namespace JDI.Light.Tests.UIObjects.Pages
{
    public class Html5Page : WebPage
    {
        private Alert alert = new Alert();

        [FindBy(Id = "blue-button")]
        public IButton BlueButton;

        [FindBy(Css = "h1")]
        public ILabel JdiLabel;

        public void ClickJdiTitle()
        {
            JdiLabel.Click();
        }
        public void ClickBlueButton()
        {
            BlueButton.Click();
        }
        public void OkAlertAction()
        {
            alert.OkAction();
        }
        public void CancelAlertAction()
        {
            alert.CancelAction();
        }

        public string GetAlertText()
        {
            return alert.GetTextAction();
        }
    }
}