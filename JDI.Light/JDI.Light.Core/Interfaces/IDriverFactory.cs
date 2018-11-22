using JDI.Core.Interfaces.Base;
using JDI.Core.Settings;

namespace JDI.Core.Interfaces
{
    public interface IDriverFactory<out T>
    {
        string CurrentDriverName { get; set; }
        string DriverPath { get; set; }
        string RegisterDriver(string driverName);

        void SetRunType(string runType);

        T GetDriver();

        bool HasDrivers();

        bool HasRunDrivers();

        T GetDriver(string name);

        void Highlight(IBaseUIElement element);

        void Highlight(IBaseUIElement element, HighlightSettings highlightSettings);
    }
}