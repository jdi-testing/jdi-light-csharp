using System;
using System.IO;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;

namespace JDI.Light.Tests.UITests
{
    public class TestBase
    {
        [TearDown]
        public void TestTearDown()
        {
            var folder = @"C:\Screenshots";
            Directory.CreateDirectory(folder);
            var res = TestContext.CurrentContext.Result.Outcome;
            if (res.Equals(ResultState.Failure) || res.Equals(ResultState.Error))
            {
                JDI.DriverFactory.GetDriver().TakeScreenshot()
                    .SaveAsFile(Path.Combine(folder, $"{Guid.NewGuid()}.png"), ScreenshotImageFormat.Png);
            }
        }
    }
}