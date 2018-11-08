using JDI.Core.Settings;
using JDI.UIWebTests.UIObjects;
using NUnit.Framework;

namespace JDI.UIWebTests.Tests.Composite
{
    public class SearchTests
    {

        [SetUp]
        public void SetUp()
        {
            JDISettings.Logger.Info("Navigating to Home page.");
            TestSite.HomePage.Open();
            TestSite.HomePage.CheckTitle();
            TestSite.HomePage.IsOpened();
            JDISettings.Logger.Info("Setup method finished");
            JDISettings.Logger.Info("Start test: " + TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void FillTest()
        {
            TestSite.Header.Search.SearchButton.Click();
            TestSite.Header.Search.Find("something");
            TestSite.Header.Search.SearchButonActive.Click();
            TestSite.SupportPage.CheckOpened();
        }

    }
}
