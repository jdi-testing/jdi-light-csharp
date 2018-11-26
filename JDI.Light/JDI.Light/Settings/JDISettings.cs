using System;
using JDI.Core.Interfaces;
using JDI.Core.Logging;
using JDI.Core.Utils;

// ReSharper disable InconsistentNaming

namespace JDI.Core.Settings
{
    public class JDISettings
    {
        public static ILogger Logger;
        public static IAssert Asserter;
        public static WebTimeoutSettings Timeouts = new WebTimeoutSettings();
        public static bool IsDemoMode;
        public static HighlightSettings HighlightSettings = new HighlightSettings();
        public static bool ShortLogMessagesFormat = true;
        public static string JDISettingsPath = "test.properties";
        public static bool ExceptionThrown;
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

        public static string UseDriver(string driverName)
        {
            return DriverFactory.RegisterDriver(driverName);
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
            //var b = System.Configuration.ConfigurationManager.AppSettings["DriversFolder"];
            //var a = Properties.Settings.Default["DriversFolder"];
            ExceptionUtils.AvoidExceptions(() => action.Invoke(Properties.Settings.Default[name].ToString()));
        }

        public static void InitFromProperties(string propertyPath)
        {
            JDISettingsPath = propertyPath;
            InitFromProperties();
        }

        public static void NewTest()
        {
            ExceptionThrown = false;
        }

        public static Exception Exception(string msg, Exception ex)
        {
            ExceptionThrown = true;
            return Asserter.Exception(msg, ex);
        }

        public static Exception Exception(string msg)
        {
            ExceptionThrown = true;
            return Asserter.Exception(msg);
        }
    }
}