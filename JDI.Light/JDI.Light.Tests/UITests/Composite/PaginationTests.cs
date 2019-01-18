using JDI.Light.Elements.Composite;
using JDI.Light.Tests.UIObjects;
using NUnit.Framework;

namespace JDI.Light.Tests.UITests.Composite
{
    [TestFixture]
    public class PaginationTests : TestBase
    {
        private readonly Pagination _simplePagePaginator = TestSite.SimpleTablePage.Paginator;

        [SetUp]
        public void SetUp()
        {
            Jdi.Logger.Info("Navigating to Simple Table page.");
            TestSite.SimpleTablePage.Open();
            TestSite.SimpleTablePage.CheckTitle();
            TestSite.SimpleTablePage.IsOpened();
            Jdi.Logger.Info("Setup method finished");
            Jdi.Logger.Info("Start test: " + TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void NextTest()
        {
            _simplePagePaginator.Next();
            Assert.True(Jdi.WebDriver.Url.Contains("user-table.html"));
        }
        
        [Test]
        public void PrevTest()
        {
            _simplePagePaginator.Previous();
            Assert.True(Jdi.WebDriver.Url.Contains("complex-table.html"));
        }
        
        [Test]
        public void FirstTest()
        {
            _simplePagePaginator.First();
            Assert.True(Jdi.WebDriver.Url.Contains("support.html"));
        }
        
        [Test]
        public void LastTest()
        {
            _simplePagePaginator.Last();
            Assert.True(Jdi.WebDriver.Url.Contains("performance.html"));
        }
    }
}