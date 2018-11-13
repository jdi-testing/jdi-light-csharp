using JDI.Core.Selenium.Elements.Composite;
using JDI.Core.Settings;
using JDI.UIWebTests.UIObjects;
using NUnit.Framework;

namespace JDI.UIWebTests.Tests.Composite
{
    public class PaginationTests
    {
        private readonly Pagination simplePagePaginator = TestSite.SimpleTablePage.Paginator;

        [SetUp]
        public void SetUp()
        {
            JDISettings.Logger.Info("Navigating to Simple Table page.");
            TestSite.SimpleTablePage.Open();
            TestSite.SimpleTablePage.CheckTitle();
            TestSite.SimpleTablePage.IsOpened();
            JDISettings.Logger.Info("Setup method finished");
            JDISettings.Logger.Info("Start test: " + TestContext.CurrentContext.Test.Name);
        }

        private void CheckPageOpened(int num)
        {
            Assert.True(WebSettings.WebDriver.Url.Contains("/page" + num + ".htm"));
        }

        [Test]
        public void NextTest()
        {
            simplePagePaginator.Next();
            CheckPageOpened(7);
        }


        [Test]
        public void PrevTest()
        {
            simplePagePaginator.Previous();
            CheckPageOpened(5);
        }


        [Test]
        public void FirstTest()
        {
            simplePagePaginator.First();
            CheckPageOpened(1);
        }


        [Test]
        public void LastTest()
        {
            simplePagePaginator.Last();
            CheckPageOpened(2);
        }
    }
}