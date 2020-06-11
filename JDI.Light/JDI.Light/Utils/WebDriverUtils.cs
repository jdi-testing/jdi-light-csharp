using System;
using System.IO;
using JDI.Light.Enums;
using JDI.Light.Extensions;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager.Helpers;

namespace JDI.Light.Utils
{
    public class WebDriverUtils
    {
        /// <summary>
        ///     Checks if local driver version is same with driver version from repo
        /// </summary>
        /// <param name="type">Driver type</param>
        /// <param name="executingPath">Driver location path</param>
        /// <param name="version">
        ///     If version specified, checks if local driver version is equals with specified driver version from
        ///     repo
        /// </param>
        /// <returns>True - drivers version is same, else false</returns>
        public static bool IsLocalVersionLatestVersion(DriverType type, string executingPath, string version = "")
        {
            var result = false;
            var latestVersion = version == "" ? GetLatestVersionNumber(type) : version;
            string driverBinaryName;
            switch (type)
            {
                case DriverType.Chrome:
                    driverBinaryName = new ChromeConfig().GetBinaryName();
                    result = Path.Combine(executingPath, driverBinaryName).CheckDriverVersionFromExe(latestVersion);
                    break;
                case DriverType.Firefox:
                    driverBinaryName = new FirefoxConfig().GetBinaryName();
                    result = Path.Combine(executingPath, driverBinaryName).CheckDriverVersionFromExe(latestVersion);
                    break;
                case DriverType.IE:
                    driverBinaryName = new InternetExplorerConfig().GetBinaryName();
                    result = Path.Combine(executingPath, driverBinaryName).CheckDriverVersionFormExeAttributes(latestVersion);
                    break;
                default: break;
            }

            return result;
        }

        /// <summary>
        ///     Get's latest driver version number
        /// </summary>
        /// <param name="type">Driver type</param>
        /// <returns>Version number</returns>
        private static string GetLatestVersionNumber(DriverType type)
        {
            var latestVersion = "";
            switch (type)
            {
                case DriverType.Chrome:
                    latestVersion = new ChromeConfig().GetLatestVersion();
                    break;
                case DriverType.Firefox:
                    latestVersion = new FirefoxConfig().GetLatestVersion();
                    break;
                case DriverType.IE:
                    latestVersion = new InternetExplorerConfig().GetLatestVersion();
                    break;
            }

            return latestVersion;
        }

        /// <summary>
        ///     Downloads driver from repo
        /// </summary>
        /// <param name="type">Driver type</param>
        /// <param name="version">Version number</param>
        /// <returns>Path of driver location</returns>
        private static string GetDriverVersion(DriverType type, string version = "")
        {
            var executingPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory);
            var binaryName = "";
            var url = "";
            var latestVersionNumber = version == "" ? GetLatestVersionNumber(type) : version;
            var currentArchitecture = ArchitectureHelper.GetArchitecture();
            switch (type)
            {
                case DriverType.Chrome:
                    var cConfig = new ChromeConfig();
                    binaryName = cConfig.GetBinaryName();
                    url = currentArchitecture == Architecture.X32 ? cConfig.GetUrl32() : cConfig.GetUrl64();
                    url = UrlHelper.BuildUrl(url, latestVersionNumber);
                    break;
                case DriverType.Firefox:
                    var fConfig = new FirefoxConfig();
                    binaryName = fConfig.GetBinaryName();
                    url = currentArchitecture == Architecture.X32 ? fConfig.GetUrl32() : fConfig.GetUrl64();
                    url = UrlHelper.BuildUrl(url, latestVersionNumber);
                    break;
                default:
                    var ieConfig = new InternetExplorerConfig();
                    binaryName = ieConfig.GetBinaryName();
                    url = currentArchitecture == Architecture.X32 ? ieConfig.GetUrl32() : ieConfig.GetUrl64();
                    url = UrlHelper.BuildUrl(url, latestVersionNumber);
                    break;
            }

            var driverLocationPath = version == ""
                ? Path.Combine(executingPath, type.ToString())
                : Path.Combine(Path.Combine(executingPath, type.ToString()), version);

            var driverFullPath = Path.Combine(driverLocationPath, binaryName);

            if (!IsLocalVersionLatestVersion(type, driverLocationPath, version))
            {
                if (File.Exists(driverFullPath))
                {
                    File.Delete(driverFullPath);
                }
                new WebDriverManager.DriverManager().SetUpDriver(url, driverFullPath, binaryName);
            }
            return driverLocationPath;
        }

        /// <summary>
        ///     Downloads specified driver version
        /// </summary>
        /// <param name="type">Driver Type</param>
        /// <param name="version">Version Number</param>
        /// <returns>Path of driver location</returns>
        public static string GetSpecifiedVersion(DriverType type, string version = "")
        {
            return GetDriverVersion(type, version);
        }

        /// <summary>
        ///     Downloads latest driver version
        /// </summary>
        /// <param name="type">Driver type</param>
        /// <returns>Path of driver location</returns>
        public static string GetLatestVersion(DriverType type)
        {
            return GetDriverVersion(type);
        }
    }
}