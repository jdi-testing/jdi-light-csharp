using JDI.Light.Interfaces.Base;

namespace JDI.Light.Interfaces.Common
{
    public interface IImage : IBaseUIElement
    {
        string Src { get; }
        string Alt { get; }
        string Height { get; }
        string Width { get; }
    }
}