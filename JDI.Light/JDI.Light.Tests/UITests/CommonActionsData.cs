using System;
using System.Threading;
using System.Threading.Tasks;
using JDI.Light.Tests.UIObjects;

namespace JDI.Light.Tests.UITests
{
    public class CommonActionsData
    {
        public static int WaitTimeout => 500;

        public static void CheckText(Func<string> func, string expectedAttrValue)
        {
            Jdi.Assert.AreEquals(func(), expectedAttrValue);
        }

        public static void CheckAction(TestSite site, string text)
        {
            var logOutput = site.ActionsLog.Texts;
            Jdi.Assert.Contains(logOutput[0], text);
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