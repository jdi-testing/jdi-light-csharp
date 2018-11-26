using JDI.Light.Selenium.Elements.Complex.Table;
using JDI.Light.Tests.Tests.Complex.Table.Base;
using NUnit.Framework;
using Assert = JDI.Light.Tests.Asserts.Assert;

namespace JDI.Light.Tests.Tests.Complex.Table
{
    [TestFixture]
    internal class GetCellTests : SupportTableTestBase
    {
        private readonly string _cellValue = "Log4J, TestNG log, Custom";

        [Test]
        public void GetCellIntIntTest()
        {
            Assert.AreEquals(Table.Cell(2, 4).GetText, _cellValue);
        }

        [Test]
        public void GetCellParamsIntIntTest()
        {
            Assert.AreEquals(Table.Cell(Column.column(2), Row.CreateRow(4)).GetText, _cellValue);
        }

        [Test]
        public void GetCellParamsIntStringTest()
        {
            Assert.AreEquals(Table.Cell(Column.column(2), Row.CreateRow("4")).GetText, _cellValue);
        }

        [Test]
        public void GetCellParamsStringIntTest()
        {
            Assert.AreEquals(Table.Cell(Column.column("Now"), Row.CreateRow(4)).GetText, _cellValue);
        }

        [Test]
        public void GetCellParamsStringStringTest()
        {
            Assert.AreEquals(Table.Cell(Column.column("Now"), Row.CreateRow("4")).GetText, _cellValue);
        }

        [Test]
        public void GetCellStringStringTest()
        {
            Assert.AreEquals(Table.Cell("Now", "4").GetText, _cellValue);
        }
    }
}