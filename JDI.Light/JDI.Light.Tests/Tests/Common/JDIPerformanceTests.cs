using System;
using NUnit.Framework;
using static JDI.Light.Elements.Complex.Table.TableMatcher;
using static JDI.Light.Elements.Complex.Table.Column;
using System.Diagnostics;

namespace JDI.Light.Tests.Tests.Common
{
    [TestFixture]
    public class JDIPerformanceTests : TestBase
    {
        [SetUp]
        public void SetUp()
        {
            TestSite.PerformancePage.Open();
            TestSite.PerformancePage.CheckOpened();
        }

        [Test]
        public void HugeTableSearchTest()
        {
            TestSite.PerformancePage.UsersTable.AssertThat().HasRowWithValues(
                ContainsValue("Meyer", InColumn("Name")),
                ContainsValue("co.uk", InColumn("Email")));
            TestSite.PerformancePage.UsersTable.AssertThat().HasRowWithValues(
                ContainsValue("Meyer", InColumn("Name")),
                ContainsValue("co.uk", InColumn("Email")));

            var timer = Stopwatch.StartNew();
            var row = TestSite.PerformancePage.UsersTable.Row(
                ContainsValue("Meyer", InColumn("Name")),
                ContainsValue("co.uk", InColumn("Email")));
            Console.WriteLine("Huge table search test Time: " + timer.Elapsed);
            Jdi.Assert.AreEquals(row.GetValue(),
                "Brian Meyer;(016977) 0358;mollis.nec@seddictumeleifend.co.uk;Houston");
        }
    }
}
