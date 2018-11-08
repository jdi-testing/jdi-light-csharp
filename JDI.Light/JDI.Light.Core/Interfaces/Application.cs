using JDI.Core.Preconditions;
using JDI.Core.Settings;
using NUnit.Framework;

namespace JDI.Core.Interfaces
{
    public class Application
    {
        private string driverName;

        public void SetDriverName(string driverName)
        {
            this.driverName = driverName;
        }

        public void IsInState(IPreconditions precondition)
        {
            JDISettings.DriverFactory.CurrentDriverName = driverName;
            PreconditionsState.IsInState(precondition);
        }
        public void IsInState(IPreconditions precondition, DescriptionAttribute method)
        {
            JDISettings.DriverFactory.CurrentDriverName = driverName;
            PreconditionsState.IsInState(precondition, method);
        }
    }
}
