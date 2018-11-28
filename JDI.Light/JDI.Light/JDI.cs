using System;
using JDI.Light.Selenium.Elements;
using JDI.Light.Settings;
using static JDI.Light.Utils.ExceptionUtils;

namespace JDI.Light
{
    public static class JDI
    {
        public static bool IsDemoMode;
        public static HighlightSettings HighlightSettings;
        public static bool ShortLogMessagesFormat;
        public static bool UseCache;
        public static WebSettings WebSettings;

        static JDI()
        {
            WebSettings.Timeouts = new WebTimeoutSettings();
            HighlightSettings = new HighlightSettings();
            ShortLogMessagesFormat = true;
            WebSettings = new WebSettings();

            GetFromPropertiesAvoidExceptions(p => WebSettings.DriverFactory.RegisterDriver(p), "Driver");
            GetFromPropertiesAvoidExceptions(p => WebSettings.DriverFactory.SetRunType(p), "RunType");
            GetFromPropertiesAvoidExceptions(p => WebSettings.Timeouts.WaitElementSec = int.Parse(p), "TimeoutWaitElement");
            GetFromPropertiesAvoidExceptions(p => ShortLogMessagesFormat = p.ToLower().Equals("short"), "LogMessageFormat");
            GetFromPropertiesAvoidExceptions(p =>
                UseCache = p.ToLower().Equals("true") || p.ToLower().Equals("1"), "Cache");
            GetFromPropertiesAvoidExceptions(p =>
                UseCache = p.ToLower().Equals("true") || p.ToLower().Equals("1"), "DemoMode");
            GetFromPropertiesAvoidExceptions(p => HighlightSettings.SetTimeoutInSec(int.Parse(p)), "DemoDelay");
        }
        
        public static void Init(Type siteType)
        {
            WebCascadeInit.InitStaticPages(siteType, WebSettings.WebDriverFactory.CurrentDriverName);
        }
    }
}