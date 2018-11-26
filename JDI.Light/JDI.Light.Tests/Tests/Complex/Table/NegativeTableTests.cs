using System.Collections.Generic;
using JDI.Light.Tests.DataProviders;
using JDI.Light.Tests.Tests.Complex.Table.Base;
using NUnit.Framework;

namespace JDI.Light.Tests.Tests.Complex.Table
{
    [TestFixture]
    internal class NegativeTableTests : SupportTableTestBase
    {
        [Test]
        [TestCaseSource(typeof(IndexesProvider), nameof(IndexesProvider.Indexes))]
        public void IllegalColumnIndexTest(int columnIndex)
        {
            Assert.Throws<AssertionException>(() => Table.Column(columnIndex));
        }

        [Test]
        public void IllegalHeaderIndexTest()
        {
            Assert.Throws<KeyNotFoundException>(() =>
            {
                var e = Table.Rows.Header("Row_illegal").WebElement;
            });
        }

        [Test]
        public void IllegalHeaderNameTest()
        {
            Assert.Throws<KeyNotFoundException>(() =>
            {
                var e = Table.Header("Column_illegal").WebElement;
            });
        }

        [Test]
        [TestCaseSource(typeof(IndexesProvider), nameof(IndexesProvider.Indexes))]
        public void IllegalRowIndexTest(int rowIndex)
        {
            // ExpectedException attribute no longer supported in NUnit 3
            Assert.Throws<AssertionException>(() => Table.Row(rowIndex));
        }
    }
}