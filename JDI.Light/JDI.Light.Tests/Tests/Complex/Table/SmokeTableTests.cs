using System.Linq;
using JDI.Light.Extensions;
using JDI.Light.Tests.Asserts;
using JDI.Light.Tests.Tests.Complex.Table.Base;
using NUnit.Framework;

namespace JDI.Light.Tests.Tests.Complex.Table
{
    [TestFixture]
    internal class SmokeTableTests : SupportTableTestBase
    {
        [Test]
        public void TableColumnHeadersTest()
        {
            new NUnitAsserter("Column headers").AreEquals("Type, Now, Plans", Table.Columns.Headers.FormattedJoin());
        }

        [Test]
        public void TableDimensionTest()
        {
            new NUnitAsserter("Dimensions").AreEquals("3/6", $"{Table.Columns.Count}/{Table.Rows.Count}");
        }

        [Test]
        public void TableHeadersAsTextTest()
        {
            new NUnitAsserter("Table header as text").AreEquals("Type, Now, Plans",
                Table.Header().Select(p => p.Value.GetText).FormattedJoin());
        }

        [Test]
        public void TableHeadersTest()
        {
            new NUnitAsserter("Table headers").AreEquals("Type, Now, Plans", Table.Headers.FormattedJoin());
        }

        [Test]
        public void TableIsNotEmptyTest()
        {
            new NUnitAsserter("Table not empty").IsFalse(Table.Empty);
        }

        [Test]
        public void TableRowHeadersTest()
        {
            new NUnitAsserter("Row headers").AreEquals("1, 2, 3, 4, 5, 6", Table.Rows.Headers.FormattedJoin());
        }
    }
}