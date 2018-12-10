using System;
using System.IO;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;

namespace JDI.Light.Tests.Tests
{
    public class TestBase
    {
        [TearDown]
        public void TestTearDown()
        {
            var res = TestContext.CurrentContext.Result.Outcome;
            if (res.Equals(ResultState.Failure) || res.Equals(ResultState.Error))
            {
                var folder = @"C:\Screenshots";
                Directory.CreateDirectory(folder);
                JDI.DriverFactory.GetDriver().TakeScreenshot()
                    .SaveAsFile(Path.Combine(folder, $"{Guid.NewGuid()}.png"), ScreenshotImageFormat.Png);
            }
        }
    }
}