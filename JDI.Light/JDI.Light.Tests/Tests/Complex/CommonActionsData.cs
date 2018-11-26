using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using JDI.Light.Settings;
using JDI.Light.Tests.Asserts;
using JDI.Light.Tests.UIObjects;
using OpenQA.Selenium;

namespace JDI.Light.Tests.Tests.Complex
{
    public class CommonActionsData
    {
        public static string NoElementsMessage =>
            "No elements selected. Override getSelectedAction or place locator to <select> tag";

        public static int WaitTimeout => 1000;

        /// <summary>
        ///     Check result of calculation on "Metals and Colors" page
        /// </summary>
        /// <param name="text"></param>
        public static void CheckCalculate(string text)
        {
            new Check().Contains(TestSite.MetalsColorsPage.CalculateText.GetText, text);
        }

        public static void CheckText(Func<string> func, string expectedAttrValue)
        {
            Assert.AreEquals(func(), expectedAttrValue);
        }

        public static void CheckAction(string text)
        {
            IList<IWebElement> logOutput = TestSite.HomePage.WebDriver.FindElements(By.CssSelector(".logs li"));
            new Check().Contains(logOutput[0].Text, text);
        }

        public static void CheckResult(string text)
        {
            new Check().Contains(TestSite.ContactFormPage.Result.GetText, text);
        }

        public static void CheckActionThrowError(Action checkedAction, string message)
        {
            try
            {
                checkedAction();
            }
            catch (Exception ex)
            {
                Assert.Contains(ex.Message, message);
                return;
            }

            throw JDISettings.Exception("Exception not thrown");
        }

        public static void RunParallel(Action action)
        {
            Task.Run(() =>
            {
                Thread.Sleep(WaitTimeout);
                action();
            });
        }
    }
}