using JDI.Light.Interfaces.Base;

namespace JDI.Light.Interfaces.Common
{
    public interface IDateTimeSelector
    {
        void SetDateTime(string value);
        string GetValue();
    }
}