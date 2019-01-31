using JDI.Light.Elements.Composite;
using NUnit.Framework;

namespace JDI.Light.Tests.UITests.Composite
{
    [TestFixture]
    public class PaginationTests : TestBase
    {
        private Pagination SimplePagePaginator => TestSite.SimpleTablePage.Paginator;

        [SetUp]
        public void SetUp()
        {
            Jdi.Logger.Info("Navigating to Simple Table page.");
            TestSite.SimpleTablePage.Open();
            TestSite.SimpleTablePage.CheckTitle();
            Jdi.Logger.Info("Setup method finished");
            Jdi.Logger.Info("Start test: " + TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void NextTest()
        {
            SimplePagePaginator.Next();
            Assert.True(Jdi.WebDriver.Url.Contains("user-table.html"));
        }
        
        [Test]
        public void PrevTest()
        {
            SimplePagePaginator.Previous();
            Assert.True(Jdi.WebDriver.Url.Contains("complex-table.html"));
        }
        
        [Test]
        public void FirstTest()
        {
            SimplePagePaginator.First();
            Assert.True(Jdi.WebDriver.Url.Contains("support.html"));
        }
        
        [Test]
        public void LastTest()
        {
            SimplePagePaginator.Last();
            Assert.True(Jdi.WebDriver.Url.Contains("performance.html"));
        }
    }
}