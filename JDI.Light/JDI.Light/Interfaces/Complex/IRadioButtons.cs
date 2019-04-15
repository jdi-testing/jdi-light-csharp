using JDI.Light.Interfaces.Base;

namespace JDI.Light.Interfaces.Complex
{
    public interface IRadioButtons : IBaseUIElement
    {
        void Select(string value);
        void Select(int index);
        new string Selected();
        string[] Values();
    }
}