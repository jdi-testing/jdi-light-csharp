using JDI.Light.Tests.UIObjects.Forms;
using JDI.Light.Tests.UIObjects.Pages;
using NUnit.Framework;
using OpenQA.Selenium;

namespace JDI.Light.Tests.Tests.Composite
{
    [TestFixture]
    public class WebSiteTests : TestBase
    {
        [Test]
        public void GetGenericElementTest()
        {
            TestSite.ContactFormPage.Open();
            var form = TestSite.ContactFormPage.Get<ContactForm>(By.CssSelector("main form"));
            form.DescriptionField.Value = "some text";
            Assert.AreEqual("some text", TestSite.ContactFormPage.ContactForm.DescriptionField.Text);
        }

        [Test]
        public void TitleTest()
        {
            var title = TestSite.Title;
            Assert.AreEqual("Home Page", title);
        }

        [Test]
        public void OpenPageTest()
        {
            var page = TestSite.Get<HomePage>("/index.html", "Home Page");
            page.Open();
            page.CheckOpened();
        }

        [Test]
        public void OpenUrlTest()
        {
            var url = TestSite.Domain + "/support.html";
            TestSite.OpenUrl(url);
            Assert.AreEqual(url, TestSite.Url);
        }

        [Test]
        public void OpenBaseUrlTest()
        {
            TestSite.OpenBaseUrl();
            Assert.AreEqual("https://epam.github.io/", TestSite.Url);
        }
        
        [Test]
        public void RefreshTest()
        {
            TestSite.ContactFormPage.Open();
            TestSite.ContactFormPage.CheckOpened();
            TestSite.ContactFormPage.ContactSubmit.Click();
            Jdi.Assert.Contains(TestSite.ContactFormPage.Result.Value, "Summary: 3");
            TestSite.Refresh();
            Jdi.Assert.AreEquals(TestSite.ContactFormPage.Result.Value, "");
            TestSite.ContactFormPage.CheckOpened();
        }

        [Test]
        public void BackTest()
        {
            TestSite.ContactFormPage.Open();
            TestSite.ContactFormPage.CheckOpened();
            TestSite.HomePage.Open();
            TestSite.HomePage.CheckOpened();
            TestSite.Back();
            TestSite.ContactFormPage.CheckOpened();
        }

        [Test]
        public void ForwardTest()
        {
            TestSite.ContactFormPage.Open();
            TestSite.ContactFormPage.CheckOpened();
            TestSite.HomePage.Open();
            TestSite.HomePage.CheckOpened();
            TestSite.Back();
            TestSite.ContactFormPage.CheckOpened();
            TestSite.Forward();
            TestSite.HomePage.CheckOpened();
        }
    }
}