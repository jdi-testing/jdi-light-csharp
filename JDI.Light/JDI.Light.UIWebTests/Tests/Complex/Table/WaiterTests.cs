using JDI.Matchers.NUnit;
using JDI.UIWebTests.Tests.Complex.Table.Base;
using JDI.UIWebTests.UIObjects;
using JDI.Web.Selenium.Elements.Complex.Table;
using NUnit.Framework;
using Assert = JDI.Matchers.NUnit.Assert;

namespace JDI.UIWebTests.Tests.Complex.Table
{
    [TestFixture]
    class WaiterTests : SupportTableTestBase
    {
        [Test]
        public void WaitExpectedRowsValueTest()
        {
            new Check("Find value").IsTrue(Table.WaitValue("Cucumber, Jbehave, Thucydides, SpecFlow", Row.row(6)));
        }

        [Test]
        public void WaitUnexpectedRowsValueTest()
        {
            new Check("Do not find value").IsFalse(Table.WaitValue("Cucumber, Jbehave, Thucydides, SpecFlow Unexepected", Row.row(6)));
        }

        [Test]
        public void WaitExpectedColumnsValueTest()
        {
            new Check("Find value").IsTrue(Table.WaitValue("Custom", Column.column(2)));
        }

        [Test]
        public void WaitUnexpectedColumnsValueTest()
        {
            new Check("Do not find value").IsFalse(Table.WaitValue("Custom Unexepected", Column.column(2)));
        }

        [Test]
        public void TableIsEmptyTest()
        {
            new Check("Table not empty").IsFalse(Table.Empty);
        }

        [Test]
        public void CellWaitTextTest()
        {
            TestSite.HomePage.IsOpened();
            CommonActionsData.RunParallel(() => TestSite.SupportPage.IsOpened());
            CommonActionsData.CheckText(() => Table.Cell(2, 2).WaitText("TestNG, JUnit, Custom"), "TestNG, JUnit, Custom");
        }

        [Test]
        public void CellWaitMatchTextTest()
        {
            TestSite.HomePage.IsOpened();
            CommonActionsData.RunParallel(() => TestSite.SupportPage.IsOpened());
            CommonActionsData.CheckText(() => Table.Cell(2, 2).WaitMatchText("[a-zA-Z, ]*JUnit,[a-zA-Z ]*"), "TestNG, JUnit, Custom");
        }

        [Test]
        public void WaitHaveRowsTest()
        {
            TestSite.HomePage.IsOpened();
            CommonActionsData.RunParallel(() => TestSite.SupportPage.IsOpened());
            Assert.IsTrue(Table.WaitHaveRows());
        }

        [Test]
        public void WaitRowsTest()
        {
            TestSite.HomePage.IsOpened();
            CommonActionsData.RunParallel(() => TestSite.SupportPage.IsOpened());
            Assert.IsTrue(Table.WaitRows(6));
        }

        [Test]
        public void WaitRowsTimeoutTest()
        {
            TestSite.HomePage.IsOpened();
            CommonActionsData.RunParallel(() => TestSite.SupportPage.IsOpened());
            Assert.IsFalse(Table.WaitRows(7));
        }
    }
}
