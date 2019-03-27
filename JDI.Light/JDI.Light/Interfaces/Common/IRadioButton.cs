using JDI.Light.Interfaces.Base;

namespace JDI.Light.Interfaces.Common
{
    public interface IRadioButton : IBaseUIElement
    {
        void Select(string value);
        void Select(int index);
        string GetSelected();
    }
}