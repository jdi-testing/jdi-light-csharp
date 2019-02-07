using System;
using System.Diagnostics;
using System.Linq;
using System.Management;
using JDI.Light.Interfaces.Utils;

namespace JDI.Light.Utils
{
    public class WinProcUtils : IKillDriver
    {
        public string[] ProcessToKill { get; set; } =
        {
            "chromedriver",
            "firefox",
            "IEDriverServer",
            "gecko"
        };

        public void KillAllRunningDrivers()
        {
            foreach (var process in ProcessToKill)
            {
                Process.GetProcessesByName(process)
                    .ToList().ForEach(KillProcessAndChildren);
            }
        }

        private void KillProcessAndChildren(Process process)
        {
            if (process.Id == 0)
            {
                return;
            }

            using (var searcher = new ManagementObjectSearcher("select * From Win32_Process Where ParentProcessID =" + process.Id))
            {
                using (var managementObjectCollection = searcher.Get())
                {
                    foreach (var managementBaseObject in managementObjectCollection)
                    {
                        var p = Process.GetProcessById(Convert.ToInt32(managementBaseObject["ProcessID"]));
                        if (!p.HasExited)
                        {
                            KillProcessAndChildren(p);
                        }
                    }
                    try
                    {
                        if (!process.HasExited)
                        {
                            process.Kill();
                        }
                    }
                    catch (ArgumentException)
                    {
                        // Process already exited.
                    }
                }
            }
        }
    }
}