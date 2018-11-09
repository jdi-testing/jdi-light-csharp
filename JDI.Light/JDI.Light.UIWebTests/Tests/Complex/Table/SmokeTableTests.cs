using System.Linq;
using JDI.Core;
using JDI.Core.Extensions;
using JDI.Matchers.NUnit;
using JDI.UIWebTests.Tests.Complex.Table.Base;
using NUnit.Framework;

namespace JDI.UIWebTests.Tests.Complex.Table
{
    [TestFixture]
    internal class SmokeTableTests : SupportTableTestBase
    {
        private readonly string _tableAsText = "||X||Type|Now|Plans||\n" +
                                               "||1||Drivers|Selenium, Custom|JavaScript, Appium, WinAPI, Sikuli||\n" +
                                               "||2||Test Runner|TestNG, JUnit, Custom|MSTest, NUnit, Epam||\n" +
                                               "||3||Asserter|TestNG, JUnit, Custom|MSTest, NUnit, Epam||\n" +
                                               "||4||Logger|Log4J, TestNG log, Custom|Epam, XML/Json logging, Hyper logging||\n" +
                                               "||5||Reporter|Jenkins, Allure, Custom|EPAM Report portal, Serenity, TimCity, Hudson||\n" +
                                               "||6||BDD/DSL|Custom|Cucumber, Jbehave, Thucydides, SpecFlow||";

        [Test]
        public void GetTextTest()
        {
            new Check("Table print").AreEquals(Table.GetText, _tableAsText);
        }

        [Test]
        public void GetValueTest()
        {
            new Check("Table print").AreEquals(Table.Value, _tableAsText);
        }

        [Test]
        public void TableColumnHeadersTest()
        {
            new Check("Column headers").AreEquals("Type, Now, Plans", Table.Columns.Headers.Print());
        }

        [Test]
        public void TableDimensionTest()
        {
            new Check("Dimensions").AreEquals("3/6", $"{Table.Columns.Count}/{Table.Rows.Count}");
        }

        [Test]
        public void TableHeadersAsTextTest()
        {
            new Check("Table header as text").AreEquals("Type, Now, Plans",
                Table.Header().Select(p => p.Value.GetText).Print());
        }

        [Test]
        public void TableHeadersTest()
        {
            new Check("Table headers").AreEquals("Type, Now, Plans", Table.Headers.Print());
        }

        [Test]
        public void TableIsNotEmptyTest()
        {
            new Check("Table not empty").IsFalse(Table.Empty);
        }

        [Test]
        public void TableRowHeadersTest()
        {
            new Check("Row headers").AreEquals("1, 2, 3, 4, 5, 6", Table.Rows.Headers.Print());
        }
    }
}