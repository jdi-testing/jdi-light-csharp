using JDI.Light.Tests.UIObjects;
using NUnit.Framework;

namespace JDI.Light.Tests.UITests.Composite
{
    [TestFixture]
    public class SearchTests : TestBase
    {
        [SetUp]
        public void SetUp()
        {
            JDI.Logger.Info("Navigating to Home page.");
            TestSite.HomePage.Open();
            TestSite.HomePage.CheckTitle();
            TestSite.HomePage.IsOpened();
            JDI.Logger.Info("Setup method finished");
            JDI.Logger.Info("Start test: " + TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void FillTest()
        {
            TestSite.Header.Search.SearchButton.Click();
            TestSite.Header.Search.Find("something");
            Assert.True(JDI.DriverFactory.GetDriver().Url.Contains("complex-table.html"));
        }
    }
}