using JDI.Light.Elements.Composite;
using NUnit.Framework;
using OpenQA.Selenium;

namespace JDI.Light.Tests.Tests.Composite
{
    [TestFixture]
    public class SectionTests : TestBase
    {
        [Test]
        public void SectionTest()
        {
            var e = TestSite.HomePage.Get<Section>(By.CssSelector(".main-title"));
            Assert.AreEqual("EPAM FRAMEWORK WISHES…", e.Text);
            Assert.AreEqual("Section", e.Name);
            Assert.AreEqual(true, e.Displayed);
            Assert.AreEqual(true, e.Enabled);
            Assert.AreEqual(false, e.Hidden);
            Assert.AreEqual(By.CssSelector(".main-title"), e.Locator);
            Assert.AreEqual(false, e.Selected);
            Assert.AreEqual("h3", e.TagName);
        }
    }
}