using JDI.Light.Interfaces.Base;

namespace JDI.Light.Interfaces.Composite
{
    public interface IWebSite : IBaseElement
    {
        string Domain { get; set; }
        bool HasDomain { get; }
    }
}