using JDI.Light.Interfaces.Base;

namespace JDI.Light.Interfaces.Common
{
    public interface IImage : IBaseUIElement
    {
        string GetSource();
        string GetAlt();
    }
}