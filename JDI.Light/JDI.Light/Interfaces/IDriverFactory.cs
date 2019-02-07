using System;
using OpenQA.Selenium;

namespace JDI.Light.Interfaces
{
    public interface IDriverFactory<out T>
    {
        Func<IWebElement, bool> ElementSearchCriteria { get; set; }
        string CurrentDriverName { get; set; }
        string DriverPath { get; set; }

        string RegisterDriver(string driverName);
        void SetRunType(string runType);
        T GetDriver();
        bool HasDrivers();
        bool HasRunDrivers();
        T GetDriver(string name);
        void Close();
    }
}