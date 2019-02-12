using JDI.Light.Elements.Base;
using JDI.Light.Interfaces.Common;
using NUnit.Framework;
using OpenQA.Selenium;

namespace JDI.Light.Tests.Tests.Common
{
    public class UIElementTests : TestBase
    {
        [Test]
        public void GenericGetTest()
        {
            const string text = "some dummy text";
            TestSite.ContactFormPage.Open();
            TestSite.ContactFormPage.CheckOpened();
            var descFieldAsInterface = TestSite.ContactFormPage.ContactForm.Get<ITextArea>(By.CssSelector("textarea#description"));
            descFieldAsInterface.Input(text);
            Assert.AreEqual(text, TestSite.ContactFormPage.ContactForm.DescriptionField.Text);
            descFieldAsInterface.Clear();
            var descFieldAsUIElement = TestSite.ContactFormPage.ContactForm.Get<UIElement>(By.CssSelector("textarea#description"));
            descFieldAsUIElement.SendKeys(text);
            Assert.AreEqual(text, TestSite.ContactFormPage.ContactForm.DescriptionField.Text);
            descFieldAsUIElement.Clear();
        }

        [Test]
        public void UIElementTest()
        {
            var e = TestSite.HomePage.Get<UIElement>(By.CssSelector(".main-title"));
            Assert.AreEqual("EPAM FRAMEWORK WISHES…", e.Text);
            Assert.AreEqual("UIElement", e.Name);
            Assert.AreEqual(true, e.Displayed);
            Assert.AreEqual(true, e.Enabled);
            Assert.AreEqual(false, e.Hidden);
            Assert.AreEqual(By.CssSelector(".main-title"), e.Locator);
            Assert.AreEqual(false, e.Selected);
            Assert.AreEqual("h3", e.TagName);
        }
    }
}