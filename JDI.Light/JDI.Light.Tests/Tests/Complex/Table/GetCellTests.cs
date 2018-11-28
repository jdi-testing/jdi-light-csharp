using JDI.Light.Selenium.Elements.Complex.Table;
using JDI.Light.Settings;
using JDI.Light.Tests.Tests.Complex.Table.Base;
using NUnit.Framework;

namespace JDI.Light.Tests.Tests.Complex.Table
{
    [TestFixture]
    internal class GetCellTests : SupportTableTestBase
    {
        private readonly string _cellValue = "Log4J, TestNG log, Custom";

        [Test]
        public void GetCellIntIntTest()
        {
            WebSettings.Assert.AreEquals(Table.Cell(2, 4).GetText, _cellValue);
        }

        [Test]
        public void GetCellParamsIntIntTest()
        {
            WebSettings.Assert.AreEquals(Table.Cell(Column.column(2), Row.CreateRow(4)).GetText, _cellValue);
        }

        [Test]
        public void GetCellParamsIntStringTest()
        {
            WebSettings.Assert.AreEquals(Table.Cell(Column.column(2), Row.CreateRow("4")).GetText, _cellValue);
        }

        [Test]
        public void GetCellParamsStringIntTest()
        {
            WebSettings.Assert.AreEquals(Table.Cell(Column.column("Now"), Row.CreateRow(4)).GetText, _cellValue);
        }

        [Test]
        public void GetCellParamsStringStringTest()
        {
            WebSettings.Assert.AreEquals(Table.Cell(Column.column("Now"), Row.CreateRow("4")).GetText, _cellValue);
        }

        [Test]
        public void GetCellStringStringTest()
        {
            WebSettings.Assert.AreEquals(Table.Cell("Now", "4").GetText, _cellValue);
        }
    }
}