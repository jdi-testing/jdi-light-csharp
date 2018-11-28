using JDI.Light.Settings;
using JDI.Light.Tests.UIObjects;
using NUnit.Framework;

namespace JDI.Light.Tests.Tests.Composite
{
    public class SearchTests
    {
        [SetUp]
        public void SetUp()
        {
            WebSettings.Logger.Info("Navigating to Home page.");
            TestSite.HomePage.Open();
            TestSite.HomePage.CheckTitle();
            TestSite.HomePage.IsOpened();
            WebSettings.Logger.Info("Setup method finished");
            WebSettings.Logger.Info("Start test: " + TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void FillTest()
        {
            TestSite.Header.Search.SearchButton.Click();
            TestSite.Header.Search.Find("something");
            TestSite.Header.Search.SearchButtonActive.Click();
            TestSite.SupportPage.CheckOpened();
        }
    }
}