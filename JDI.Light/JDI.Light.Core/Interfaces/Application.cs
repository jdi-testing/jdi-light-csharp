using JDI.Core.Preconditions;
using JDI.Core.Settings;

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
        public void IsInState(IPreconditions precondition, NUnit.Framework.DescriptionAttribute method)
        {
            JDISettings.DriverFactory.CurrentDriverName = driverName;
            PreconditionsState.IsInState(precondition, method);
        }
    }
}
