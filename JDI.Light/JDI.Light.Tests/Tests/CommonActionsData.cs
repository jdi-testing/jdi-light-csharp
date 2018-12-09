using System;
using System.Threading;
using System.Threading.Tasks;
using JDI.Light.Tests.UIObjects;

namespace JDI.Light.Tests.Tests
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
            JDI.Assert.Contains(TestSite.MetalsColorsPage.CalculateText.GetText, text);
        }

        public static void CheckText(Func<string> func, string expectedAttrValue)
        {
            JDI.Assert.AreEquals(func(), expectedAttrValue);
        }

        public static void CheckAction(string text)
        {
            var logOutput = TestSite.ActionsLog.Texts;
            JDI.Assert.Contains(logOutput[0], text);
        }

        public static void CheckResult(string text)
        {
            JDI.Assert.Contains(TestSite.ContactFormPage.Result.GetText, text);
        }

        public static void CheckActionThrowError(Action checkedAction, string message)
        {
            try
            {
                checkedAction();
            }
            catch (Exception ex)
            {
                JDI.Assert.Contains(ex.Message, message);
                return;
            }

            throw JDI.Assert.Exception("Exception not thrown");
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