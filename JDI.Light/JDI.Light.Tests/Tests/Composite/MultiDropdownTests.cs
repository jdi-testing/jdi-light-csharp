using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using static JDI.Light.Jdi;

namespace JDI.Light.Tests.Tests.Composite
{
    [TestFixture()]
    public class MultiDropdownTests : TestBase
    {
        [SetUp]
        public void SetUp()
        {
            Logger.Info("Navigating to HTML5 page.");
            TestSite.Html5Page.Open();
            TestSite.Html5Page.CheckTitle();
            Logger.Info("Setup method finished");
            Logger.Info("Start test: " + TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void ExpandMultiDropdown()
        {
            TestSite.Html5Page.MultiDropdown.Expand();
            TestSite.Html5Page.MultiDropdown.SelectElementByname("Electro");
            var a = TestSite.Html5Page.MultiDropdown.TextElements;
            var b = 5;
        }
    }
}
