using System;
using System.Diagnostics;
using System.Linq;

namespace JDI.Light.Utils
{
    public static class WinProcUtils
    {
        private static readonly string[] ProcessToKill =
        {
            "chromedriver",
            "firefox",
            "IEDriverServer",
            "gecko"
        };

        public static bool KillAllDrivers = false;
        public static DateTime? TestRunStartTime { get; private set; }
        
        public static void Init()
        {
            TestRunStartTime = DateTime.Now;
        }
        
        public static void KillAllRunningDrivers()
        {
            foreach (var process in ProcessToKill)
            {
                if (KillAllDrivers)
                {
                    Process.GetProcessesByName(process)
                        .ToList().ForEach(x => x.Kill());
                }
                else
                {
                    Process.GetProcessesByName(process)
                        .Where(x => x.StartTime > TestRunStartTime)
                        .ToList().ForEach(x => x.Kill());
                }
            }
        }
    }
}