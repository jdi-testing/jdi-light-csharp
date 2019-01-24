using System;
using System.Diagnostics;
using System.Linq;

namespace JDI.Light.Utils
{
    public static class WinProcUtils
    {
        public static DateTime? TestRunStartTime { get; set; }

        private static readonly string[] ProcessToKill =
        {
            "chromedriver",
            "firefox",
            "IEDriverServer",
            "gecko"
        };

        public static void KillAllRunningDrivers()
        {
            foreach (var process in ProcessToKill)
            {
                Process.GetProcessesByName(process)
                    .Where(x => x.StartTime > TestRunStartTime)
                    .ToList().ForEach(x => x.Kill());
            }
        }
    }
}