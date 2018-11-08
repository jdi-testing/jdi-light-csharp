using System;
using System.IO;
using JDI.Web.Selenium.DriverFactory;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager.Helpers;
using Manager = WebDriverManager;

namespace JDI.Web.Selenium.DriverManager
{
    public class WebDriverManager
    {
        /// <summary>
        /// Checks if local driver version is same with driver version from repo
        /// </summary>
        /// <param name="type">Driver type</param>
        /// <param name="executingPath">Driver location path</param>
        /// <param name="version">If version specified, checks if local driver version is equals with specified driver version from repo</param>
        /// <returns>True - drivers version is same, else false</returns>
        public static bool IsLocalVersionLatestVersion(DriverTypes type, string executingPath, string version = "")
        {
            bool result = false;
            string latestVersion = version == "" ? GetLatestVersionNumber(type) : version;
            string driverBinaryName = "";
            switch (type)
            {
                case DriverTypes.Chrome:
                    driverBinaryName = new ChromeConfig().GetBinaryName();
                    result = WebDriverManagerHelper.CheckDriverVersionFromExe(Path.Combine(executingPath, driverBinaryName), latestVersion);
                    break;
                case DriverTypes.Firefox:
                    driverBinaryName = new FirefoxConfig().GetBinaryName();
                    result = WebDriverManagerHelper.CheckDriverVersionFromExe(Path.Combine(executingPath, driverBinaryName), latestVersion);
                    break;
                case DriverTypes.IE:
                    driverBinaryName = new InternetExplorerConfig().GetBinaryName();
                    result = WebDriverManagerHelper.CheckDriverVerionFormExeAttributes(Path.Combine(executingPath, driverBinaryName), latestVersion);
                    break;
            }
            return result;
        }

        /// <summary>
        /// Get's latest driver version number
        /// </summary>
        /// <param name="type">Driver type</param>
        /// <returns>Version number</returns>
        private static string GetLatestVersionNumber(DriverTypes type)
        {
            string latestVersion = "";
            switch (type)
            {
                case DriverTypes.Chrome:
                    latestVersion = new ChromeConfig().GetLatestVersion();
                    break;
                case DriverTypes.Firefox:
                    latestVersion = new FirefoxConfig().GetLatestVersion();
                    break;
                case DriverTypes.IE:
                    latestVersion = new InternetExplorerConfig().GetLatestVersion();
                    break;
            }
            return latestVersion;
        }

        /// <summary>
        /// Downloads driver from repo
        /// </summary>
        /// <param name="type">Driver type</param>
        /// <param name="version">Version number</param>
        /// <returns>Path of driver location</returns>
        private static string GetDriverVersion(DriverTypes type, string version = "")
        {
            string driverFullPath = "";
            string executingPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory);
            string binaryName = "";
            string url = "";
            string latestVersionNumber = version == "" ? GetLatestVersionNumber(type) : version;
            Architecture currnetArchitecture = ArchitectureHelper.GetArchitecture();
            switch (type)
            {
                case DriverTypes.Chrome:
                    ChromeConfig cConfig = new ChromeConfig();
                    binaryName = cConfig.GetBinaryName();
                    url = currnetArchitecture == Architecture.X32 ? cConfig.GetUrl32() : cConfig.GetUrl64();
                    url = UrlHelper.BuildUrl(url, latestVersionNumber);
                    break;
                case DriverTypes.Firefox:
                    FirefoxConfig fConfig = new FirefoxConfig();
                    binaryName = fConfig.GetBinaryName();
                    url = currnetArchitecture == Architecture.X32 ? fConfig.GetUrl32() : fConfig.GetUrl64();
                    url = UrlHelper.BuildUrl(url, latestVersionNumber);
                    break;
                case DriverTypes.IE:
                    InternetExplorerConfig ieConfig = new InternetExplorerConfig();
                    binaryName = ieConfig.GetBinaryName();
                    url = currnetArchitecture == Architecture.X32 ? ieConfig.GetUrl32() : ieConfig.GetUrl64();    
                    url = UrlHelper.BuildUrl(url, latestVersionNumber);
                    break;
            }
            string driverLocationPath = version == "" ? Path.Combine(executingPath, type.ToString()) 
                                                      : Path.Combine(Path.Combine(executingPath, type.ToString()), version);
            driverFullPath = Path.Combine(driverLocationPath, binaryName);

            if (!IsLocalVersionLatestVersion(type, driverLocationPath, version))
            {
                if(File.Exists(driverFullPath))
                    File.Delete(driverFullPath);

                new Manager.DriverManager().SetUpDriver(url, driverFullPath, binaryName);
            }
            return driverLocationPath;
        }

        /// <summary>
        /// Downloads specified driver version
        /// </summary>
        /// <param name="type">Driver Type</param>
        /// <param name="version">Version Number</param>
        /// <returns>Path of driver location</returns>
        public static string GetSpecifiedVersion(DriverTypes type,string version)
        {
           return GetDriverVersion(type, version);
        }

        /// <summary>
        /// Downloads latest driver version
        /// </summary>
        /// <param name="type">Driver type</param>
        /// <returns>Path of driver location</returns>
        public static string GetLatestVersion(DriverTypes type)
        {
            return GetDriverVersion(type);
        }
    }
}
