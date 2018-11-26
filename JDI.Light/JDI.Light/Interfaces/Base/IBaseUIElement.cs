namespace JDI.Light.Interfaces.Base
{
    public interface IBaseUIElement : IBaseElement
    {
        string GetAttribute(string name);
        void SetAttribute(string attributeName, string value);
    }
}