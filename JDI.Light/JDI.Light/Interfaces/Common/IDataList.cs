using JDI.Light.Interfaces.Base;

namespace JDI.Light.Interfaces.Common
{
    public interface IDataList : IBaseUIElement
    {
        void Expand();
        void Select(string value);
        void Select(int index);
        string GetSelected();
    }
}