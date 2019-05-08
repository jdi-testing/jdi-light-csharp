using JDI.Light.Interfaces.Base;

namespace JDI.Light.Interfaces.Common
{
    public interface IColorPicker : IBaseUIElement
    {
        string Color();
        void SetColor(string color, bool checkEnabled = true);
    }
}