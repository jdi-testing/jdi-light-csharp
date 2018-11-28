using System;
using JDI.Light.Interfaces;
using JDI.Light.Settings;
using JDI.Light.Utils;

namespace JDI.Light
{
    public class JDI
    {
        public static ILogger Logger;
        public static IAssert Assert;
        public static WebTimeoutSettings Timeouts = new WebTimeoutSettings();
        public static bool IsDemoMode;
        public static HighlightSettings HighlightSettings = new HighlightSettings();
        public static bool ShortLogMessagesFormat = true;
        public static IDriverFactory<IDisposable> DriverFactory;
        public static bool UseCache;

        public static void InitFromProperties()
        {
            FillFromSettings(p => DriverFactory.RegisterDriver(p), "Driver");
            FillFromSettings(p => DriverFactory.SetRunType(p), "RunType");
            FillFromSettings(p => Timeouts.WaitElementSec = int.Parse(p), "TimeoutWaitElement");
            FillFromSettings(p => ShortLogMessagesFormat = p.ToLower().Equals("short"), "LogMessageFormat");
            FillFromSettings(p =>
                UseCache = p.ToLower().Equals("true") || p.ToLower().Equals("1"), "Cache");
            FillFromSettings(p =>
                UseCache = p.ToLower().Equals("true") || p.ToLower().Equals("1"), "DemoMode");
            FillFromSettings(p => HighlightSettings.SetTimeoutInSec(int.Parse(p)), "DemoDelay");
        }

        protected static void FillFromSettings(Action<string> action, string name)
        {
            ExceptionUtils.AvoidExceptions(() => action.Invoke(Properties.Settings.Default[name].ToString()));
        }
    }
}