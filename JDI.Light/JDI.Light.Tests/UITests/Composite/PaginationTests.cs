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
            JDI.Logger.Info("Navigating to Simple Table page.");
            TestSite.SimpleTablePage.Open();
            TestSite.SimpleTablePage.CheckTitle();
            TestSite.SimpleTablePage.IsOpened();
            JDI.Logger.Info("Setup method finished");
            JDI.Logger.Info("Start test: " + TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void NextTest()
        {
            _simplePagePaginator.Next();
            Assert.True(JDI.DriverFactory.GetDriver().Url.Contains("user-table.html"));
        }
        
        [Test]
        public void PrevTest()
        {
            _simplePagePaginator.Previous();
            Assert.True(JDI.DriverFactory.GetDriver().Url.Contains("complex-table.html"));
        }
        
        [Test]
        public void FirstTest()
        {
            _simplePagePaginator.First();
            Assert.True(JDI.DriverFactory.GetDriver().Url.Contains("support.html"));
        }
        
        [Test]
        public void LastTest()
        {
            _simplePagePaginator.Last();
            Assert.True(JDI.DriverFactory.GetDriver().Url.Contains("performance.html"));
        }
    }
}