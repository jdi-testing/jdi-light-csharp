using System;
using JDI.Light.Enums;
using JDI.Light.Interfaces;
using JDI.Light.Utils;

namespace JDI.Light.Settings
{
    public class JDISettings
    {
        public static ILogger Logger;
        public static IAssert Assert;
        public static WebTimeoutSettings Timeouts = new WebTimeoutSettings();
        public static bool IsDemoMode;
        public static HighlightSettings HighlightSettings = new HighlightSettings();
        public static bool ShortLogMessagesFormat = true;
        public static IDriverFactory<IDisposable> DriverFactory;
        public static bool UseCache;

        public static void ToLog(string message, LogLevel level)
        {
            switch (level)
            {
                case LogLevel.Debug:
                    Logger.Debug(message);
                    return;
                case LogLevel.Error:
                    Logger.Error(message);
                    return;
            }

            Logger.Info(message);
        }

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