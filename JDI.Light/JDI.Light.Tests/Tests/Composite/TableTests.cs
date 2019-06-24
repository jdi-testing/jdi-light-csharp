using JDI.Light.Tests.Enums;
using JDI.Light.Tests.UIObjects.Pages;
using NUnit.Framework;
using static JDI.Light.Elements.Complex.Table.Column;
using static JDI.Light.Elements.Complex.Table.TableMatcher;
using static JDI.Light.Matchers.StringMatchers.ContainsStringMatcher;
using static NUnit.Framework.Assert;
using Is = JDI.Light.Matchers.Is;

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
            PerformancePage.UsersTable.Is().HasRowWithValues(HasValue("Brian Meyer", InColumn("Name")), HasValue("(016977) 0358", InColumn("Phone")),
                HasValue("mollis.nec@seddictumeleifend.co.uk", InColumn("Email")), HasValue("Houston", InColumn("City")));
        }

        [Test]
        public void HugeTableSearchByColumnNumbersContainValuesTest()
        {
            PerformancePage.Open();
            PerformancePage.CheckOpened();
            PerformancePage.UsersTable.AssertThat().HasRowWithValues(
                ContainsValue("Burke", InColumn(1)),
                ContainsValue("ut.edu", InColumn(3)));
            PerformancePage.UsersTable.Is().HasRowWithValues(HasValue("Burke Tucker", InColumn("Name")), HasValue("076 1971 1687", InColumn("Phone")),
                HasValue("et.euismod.et@ut.edu", InColumn("Email")), HasValue("GozŽe", InColumn("City")));
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
            AreEqual("Brian Meyer;(016977) 0358;mollis.nec@seddictumeleifend.co.uk;Houston",
                row.GetValue());
            PerformancePage.UsersTable.Is().HasRowWithValues(HasValue("Brian Meyer", InColumn("Name")), HasValue("(016977) 0358", InColumn("Phone")),
                HasValue("mollis.nec@seddictumeleifend.co.uk", InColumn("Email")), HasValue("Houston", InColumn("City")));
        }

        [Test]
        public void HugeTableSearchByColumnNumbersHasValuesTest()
        {
            PerformancePage.Open();
            PerformancePage.CheckOpened();
            PerformancePage.UsersTable.AssertThat().HasRowWithValues(
                HasValue("Brian Meyer", InColumn(1)),
                HasValue("mollis.nec@seddictumeleifend.co.uk", InColumn(3)));
            PerformancePage.UsersTable.Is().HasRowWithValues( HasValue("Brian Meyer", InColumn("Name")), HasValue("(016977) 0358", InColumn("Phone")),
                HasValue("mollis.nec@seddictumeleifend.co.uk", InColumn("Email")), HasValue("Houston", InColumn("City")));
        }

        [Test]
        public void TableRowPerformanceTest()
        {
            PerformancePage.Open();
            PerformancePage.CheckOpened();
            AreEqual("Burke Tucker;076 1971 1687;et.euismod.et@ut.edu;GozŽe", PerformancePage.UsersTable.Row(1).GetValue());
            AreEqual("Burke Tucker;076 1971 1687;et.euismod.et@ut.edu;GozŽe", PerformancePage.UsersTable.Row("Burke Tucker").GetValue());
            AreEqual("Burke Tucker;076 1971 1687;et.euismod.et@ut.edu;GozŽe", PerformancePage.UsersTable.Row(Users.Name).GetValue());
            var value = PerformancePage.UsersTable.Preview();
            AreEqual("Name Phone Email City" +
                "Burke Tucker 076 1971 1687 et.euismod.et@ut.edu GozŽe" +
                "Grady Brock (011307) 16843 cursus.et@commodo.org Alcobendas" +
                "Harding Lloyd 0800 1111 neque.In.ornare@mauris.co.uk Beauvais", value.Substring(0, 194));
        }

        [Test]
        public void TableCellPerformanceTest()
        {
            PerformancePage.Open();
            PerformancePage.CheckOpened();
            AreEqual("ipsum.non.arcu@auctorullamcorper.ca", PerformancePage.UsersTable.Cell(3, 4));
            AreEqual("ipsum.non.arcu@auctorullamcorper.ca", PerformancePage.UsersTable.Cell("Email", 4));
            AreEqual("ipsum.non.arcu@auctorullamcorper.ca", PerformancePage.UsersTable.Cell(3, "Zachary Hendrix"));
            AreEqual("ipsum.non.arcu@auctorullamcorper.ca", PerformancePage.UsersTable.Cell("Email", "Zachary Hendrix"));
        }

        [Test]
        public void TableColumnPerformanceTest()
        {
            PerformancePage.Open();
            PerformancePage.CheckOpened();
            AreEqual("076 1971 1687;(011307) 16843;0", PerformancePage.UsersTable.Column(2).GetValue().Substring(0, 30));
            AreEqual("076 1971 1687;(011307) 16843;0", PerformancePage.UsersTable.Column("Phone").GetValue().Substring(0, 30));
            AreEqual("076 1971 1687;(011307) 16843;0", PerformancePage.UsersTable.Column(Users.Phone).GetValue().Substring(0, 30));
        }

        [Test]
        public void TableChainTest()
        {
            PerformancePage.Open();
            PerformancePage.CheckOpened();
            PerformancePage.UsersTable.AssertThat()
                .Size(400)
                .Size(Is.GreaterThan(399))
                .Size(Is.LessThanOrEqualTo(400))
                .Size(Is.GreaterThanOrEqualTo(400))
                .Size(Is.LessThan(401))
                .HasRowWithValues(
                    HasValue("Brian Meyer", InColumn("Name")),
                    HasValue("mollis.nec@seddictumeleifend.co.uk", InColumn("Email")))
                .NotEmpty()
                .RowsWithValues(3, ContainsValue("Baker", InColumn(1)))
                .HasColumn("Email")
                .HasColumns(new[] {"Name", "City"})
                .Columns(Is.SubsequenceOf(new[] {"Name", "City", "Phone", "Email", "Address"}))
                .Tag(ContainsString("table"))
                .Displayed()
                .Enabled()
                .Attr("id", ContainsString("users-table"))
                .CssClass(ContainsString("uui-table"))
                .Css("color", ContainsString("rgba(102, 102, 102, 1)"))
                .Text(ContainsString("Name"))
                .HasClass("stripe");
        }
    }
}