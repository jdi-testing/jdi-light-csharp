using JDI.Light.Elements.Complex.Table;
using JDI.Light.Tests.UIObjects.Pages;
using NUnit.Framework;
using static JDI.Light.Elements.Complex.Table.Column;
using static JDI.Light.Elements.Complex.Table.TableMatcher;

namespace JDI.Light.Tests.Tests.Composite
{
    [TestFixture]
    public class TableTests : TestBase
    {
        private static PerformancePage PerformancePage => TestSite.PerformancePage;

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
            var row = PerformancePage.UsersTable.Row(
                ContainsValue("Meyer", InColumn("Name")),
                ContainsValue("co.uk", InColumn("Email")));
                Assert.AreEqual("Brian Meyer;(016977) 0358;mollis.nec@seddictumeleifend.co.uk;Houston",
                    row.GetValue());
        }

        [Test]
        public void HugeTableSearchByColumnNumbersContainValuesTest()
        {
            PerformancePage.Open();
            PerformancePage.CheckOpened();
            PerformancePage.UsersTable.AssertThat().HasRowWithValues(
                ContainsValue("Burke", InColumn(1)),
                ContainsValue("ut.edu", InColumn(3)));
            var row = PerformancePage.UsersTable.Row(1);
            Assert.AreEqual("Burke Tucker;076 1971 1687;et.euismod.et@ut.edu;GozŽe",
                row.GetValue());
        }

        [Test]
        public void HugeTableSearchByColumnNamesHasValuesTest()
        {
            PerformancePage.Open();
            PerformancePage.CheckOpened();
            PerformancePage.UsersTable.AssertThat().HasRowWithValues(
                HasValue("Brian Meyer", InColumn("Name")),
                HasValue("mollis.nec@seddictumeleifend.co.uk", InColumn("Email")));
            var row = PerformancePage.UsersTable.Row(
                HasValue("Brian Meyer", InColumn("Name")),
                HasValue("mollis.nec@seddictumeleifend.co.uk", InColumn("Email")));
            Assert.AreEqual("Brian Meyer;(016977) 0358;mollis.nec@seddictumeleifend.co.uk;Houston",
                row.GetValue());
        }

        [Test]
        public void HugeTableSearchByColumnNumbersHasValuesTest()
        {
            PerformancePage.Open();
            PerformancePage.CheckOpened();
            PerformancePage.UsersTable.AssertThat().HasRowWithValues(
                HasValue("Brian Meyer", InColumn(1)),
                HasValue("mollis.nec@seddictumeleifend.co.uk", InColumn(3)));
            var row = PerformancePage.UsersTable.Row(
                ContainsValue("Meyer", InColumn("Name")),
                ContainsValue("co.uk", InColumn("Email")));
            Assert.AreEqual("Brian Meyer;(016977) 0358;mollis.nec@seddictumeleifend.co.uk;Houston",
                row.GetValue());
        }
    }
}