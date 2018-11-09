using JDI.Core.Settings;
using JDI.UIWebTests.UIObjects;
using JDI.Web.Selenium.Elements.Complex.Table.Interfaces;
using NUnit.Framework;

namespace JDI.UIWebTests.Tests.Complex.Table.Base
{
    public class SupportTableTestBase // TODO: fix TestBase (consider InitTests from Java)
    {
        protected ITable Table => TestSite.SupportPage.SupportTable;

        [SetUp]
        public void Setup()
        {
            // TODO: Preconditions
            JDISettings.Logger.Info("Navigating to Support page");
            TestSite.SupportPage.Open();
            TestSite.SupportPage.CheckTitle();
            TestSite.SupportPage.IsOpened();
            JDISettings.Logger.Info("Setup method finished");
            JDISettings.Logger.Info($"Start test: {TestContext.CurrentContext.Test.Name}");
            // End preconditions

            Table.Clear();
        }
    }
}