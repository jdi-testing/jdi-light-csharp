using JDI.Light.Tests.UIObjects.Pages;
using NUnit.Framework;
using static JDI.Light.Elements.Complex.Table.Column;
using static JDI.Light.Elements.Complex.Table.TableMatcher;

namespace JDI.Light.Tests.Tests.Composite
{
    [TestFixture]
    public class TableTests : TestBase
    {
        private PerformancePage PerformancePage => TestSite.PerformancePage;

        [Test]
        public void InitComplexTable()
        {
            Jdi.Logger.Info("Navigating to Complex Table page.");
            TestSite.ComplexTablePage.Open();
            TestSite.ComplexTablePage.CheckTitle();
            var table = TestSite.ComplexTablePage.Table;
            var headers = table.Headers;
            var body = table.Body;
            var footer = table.Footer;
            var rows = table.Rows;
            var cells = table.Cells;
        }

        [Test]
        public void InitSimpleTable()
        {
            Jdi.Logger.Info("Navigating to Simple Table page.");
            TestSite.SimpleTablePage.Open();
            TestSite.SimpleTablePage.CheckTitle();
            var table = TestSite.SimpleTablePage.Table;
            var headers = table.Headers;
            var body = table.Body;
            var footer = table.Footer;
            var rows = table.Rows;
            var cells = table.Cells;
        }

        [Test]
        public void HugeTableSearchByColumnNamesContainValuesTest()
        {
            PerformancePage.Open();
            PerformancePage.CheckOpened();
            PerformancePage.UsersTable.AssertThat().HasRowWithValues(
                ContainsValue("Meyer", InColumn("Name")),
                ContainsValue("co.uk", InColumn("Email")));
        }

        [Test]
        public void HugeTableSearchByColumnNumbersContainValuesTest()
        {
            PerformancePage.Open();
            PerformancePage.CheckOpened();
            PerformancePage.UsersTable.AssertThat().HasRowWithValues(
                ContainsValue("Meyer", InColumn(1)),
                ContainsValue("co.uk", InColumn(3)));
        }

        [Test]
        public void HugeTableSearchByColumnNamesHasValuesTest()
        {
            PerformancePage.Open();
            PerformancePage.CheckOpened();
            PerformancePage.UsersTable.AssertThat().HasRowWithValues(
                HasValue("Brian Meyer", InColumn("Name")),
                HasValue("mollis.nec@seddictumeleifend.co.uk", InColumn("Email")));
        }

        [Test]
        public void HugeTableSearchByColumnNumbersHasValuesTest()
        {
            PerformancePage.Open();
            PerformancePage.CheckOpened();
            PerformancePage.UsersTable.AssertThat().HasRowWithValues(
                HasValue("Brian Meyer", InColumn(1)),
                HasValue("mollis.nec@seddictumeleifend.co.uk", InColumn(3)));
        }
    }
}