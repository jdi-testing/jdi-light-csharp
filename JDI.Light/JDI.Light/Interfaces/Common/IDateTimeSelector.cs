using JDI.Light.Interfaces.Base;

namespace JDI.Light.Interfaces.Common
{
    public interface IDateTimeSelector : IBaseUIElement
    {
        void SetDateTime(string value);
        string GetValue();
    }
}