using NUnit.Framework;

namespace JDI.Light.Tests.Tests.Simple
{
    [TestFixture]
    public class TitleTests : TestBase
    {
        [SetUp]
        public void SetUp()
        {
            TestSite.Html5Page.Open();
            TestSite.Html5Page.CheckOpened();
        }

        private readonly string _text = "JDI TESTING PLATFORM";

        [Test]
        public void GetTextTest()
        {
            Assert.AreEqual(TestSite.Html5Page.JdiTitle.GetText(), _text);
        }

        [Test]
        public void GetValueTest()
        {
            // todo add test after HasValue interface implementation
        }

        [Test]
        public void ClickTest()
        {
            TestSite.Html5Page.JdiTitle.Click();
            Assert.AreEqual(TestSite.Html5Page.GetAlert().GetAlertText(), "JDI Title");
            TestSite.Html5Page.GetAlert().AcceptAlert();
        }
    }
}