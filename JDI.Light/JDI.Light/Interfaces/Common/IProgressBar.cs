using JDI.Light.Interfaces.Base;

namespace JDI.Light.Interfaces.Common
{
    public interface IProgressBar : IBaseUIElement
    {
        string Value();
        string Max();
    }
}