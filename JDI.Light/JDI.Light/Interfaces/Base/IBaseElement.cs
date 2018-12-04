using JDI.Light.Elements.WebActions;

namespace JDI.Light.Interfaces.Base
{
    public interface IBaseElement
    {
        ActionInvoker Invoker { get; set; }
        ILogger Logger { get; }
        string DriverName { get; set; }
        string Name { get; set; }
        IBaseElement Parent { set; get; }

        void SetUp(ILogger logger);
    }
}