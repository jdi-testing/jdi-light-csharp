using System;
using System.Diagnostics;
using System.Linq;
using System.Management;

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

        public static void KillAllRunningDrivers()
        {
            foreach (var process in ProcessToKill)
            {
                Process.GetProcessesByName(process)
                    .ToList().ForEach(x => x.KillProcessAndChildren());
            }
        }

        private static void KillProcessAndChildren(this Process process)
        {
            if (process.Id == 0)
            {
                return;
            }
            var searcher = new ManagementObjectSearcher("select * From Win32_Process Where ParentProcessID =" + process.Id);
            ManagementObjectCollection managementObjectCollection = searcher.Get();
            foreach (var managementBaseObject in managementObjectCollection)
            {
                KillProcessAndChildren(Process.GetProcessById(Convert.ToInt32(managementBaseObject["ProcessID"])));
            }
            try
            {
                process.Kill();
            }
            catch (ArgumentException)
            {
                // Process already exited.
            }
        }
    }
}