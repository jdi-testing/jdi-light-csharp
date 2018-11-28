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
            JDI.Assert.AreEquals(Table.Cell(2, 4).GetText, _cellValue);
        }

        [Test]
        public void GetCellParamsIntIntTest()
        {
            JDI.Assert.AreEquals(Table.Cell(Column.column(2), Row.CreateRow(4)).GetText, _cellValue);
        }

        [Test]
        public void GetCellParamsIntStringTest()
        {
            JDI.Assert.AreEquals(Table.Cell(Column.column(2), Row.CreateRow("4")).GetText, _cellValue);
        }

        [Test]
        public void GetCellParamsStringIntTest()
        {
            JDI.Assert.AreEquals(Table.Cell(Column.column("Now"), Row.CreateRow(4)).GetText, _cellValue);
        }

        [Test]
        public void GetCellParamsStringStringTest()
        {
            JDI.Assert.AreEquals(Table.Cell(Column.column("Now"), Row.CreateRow("4")).GetText, _cellValue);
        }

        [Test]
        public void GetCellStringStringTest()
        {
            JDI.Assert.AreEquals(Table.Cell("Now", "4").GetText, _cellValue);
        }
    }
}