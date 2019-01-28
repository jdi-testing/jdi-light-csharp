using NUnit.Framework;

namespace JDI.Light.Tests.UITests.Composite
{
    [TestFixture]
    public class TableTests : TestBase
    {
        [Test]
        public void InitComplexTable()
        {
            Jdi.Logger.Info("Navigating to Complex Table page.");
            TestSite.ComplexTablePage.Open();
            TestSite.ComplexTablePage.CheckTitle();
            TestSite.ComplexTablePage.IsOpened();
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
            TestSite.SimpleTablePage.IsOpened();
            var table = TestSite.SimpleTablePage.Table;
            var headers = table.Headers;
            var body = table.Body;
            var footer = table.Footer;
            var rows = table.Rows;
            var cells = table.Cells;
        }
    }
}