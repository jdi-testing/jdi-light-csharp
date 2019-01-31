using NUnit.Framework;

namespace JDI.Light.Tests.UITests.Composite
{
    [TestFixture]
    public class SearchTests : TestBase
    {
        [SetUp]
        public void SetUp()
        {
            Jdi.Logger.Info("Navigating to Home page.");
            TestSite.HomePage.Open();
            TestSite.HomePage.CheckTitle();
            Jdi.Logger.Info("Setup method finished");
            Jdi.Logger.Info("Start test: " + TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void FillTest()
        {
            TestSite.Header.Search.SearchButton.Click();
            TestSite.Header.Search.Find("something");
            Assert.True(Jdi.WebDriver.Url.Contains("complex-table.html"));
        }
    }
}