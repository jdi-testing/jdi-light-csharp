using System;
using System.Threading;
using System.Threading.Tasks;
using JDI.Light.Tests.UIObjects;

namespace JDI.Light.Tests.UITests
{
    public class CommonActionsData
    {
        public static int WaitTimeout => 1000;

        public static void CheckText(Func<string> func, string expectedAttrValue)
        {
            JDI.Assert.AreEquals(func(), expectedAttrValue);
        }

        public static void CheckAction(string text)
        {
            var logOutput = TestSite.ActionsLog.Texts;
            JDI.Assert.Contains(logOutput[0], text);
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