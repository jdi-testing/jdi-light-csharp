using System;
using JDI.Light.Tests.UIObjects;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;

namespace JDI.Light.Tests.UITests.Common
{
    [TestFixture]
    public class ButtonTests : TestBase
    {
        [SetUp]
        public void SetUp()
        {
            JDI.Logger.Info("Navigating to Metals and Colors page.");
            TestSite.MetalsColorsPage.Open();
            TestSite.MetalsColorsPage.CheckTitle();
            TestSite.MetalsColorsPage.IsOpened();
            JDI.Logger.Info("Setup method finished");
            JDI.Logger.Info("Start test: " + TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void ClickTest()
        {
            TestSite.MetalsColorsPage.CalculateButton.Click();
            var calcText = TestSite.MetalsColorsPage.CalculateText.Value;
            JDI.Assert.Contains(calcText, "Summary: 3");
            JDI.DriverFactory.GetDriver().TakeScreenshot().SaveAsFile($"{Guid.NewGuid()}.png", ScreenshotImageFormat.Png);
        }
    }
}