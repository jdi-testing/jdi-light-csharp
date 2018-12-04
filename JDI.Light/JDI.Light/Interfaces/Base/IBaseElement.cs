using JDI.Light.Elements.WebActions;

namespace JDI.Light.Interfaces.Base
{
    public interface IBaseElement
    {
        ActionInvoker Invoker { get; set; }
        ILogger Logger { get; set; }
        string DriverName { get; set; }
        string Name { get; set; }
        IBaseElement Parent { set; get; }
    }
}