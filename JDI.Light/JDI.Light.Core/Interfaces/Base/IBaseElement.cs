namespace JDI.Core.Interfaces.Base
{
    public interface IBaseElement
    {
        string DriverName { get; set; }
        string Name { get; set; }
        string TypeName { get; }
        object Parent { set; get; }
        string GetAttribute(string name);
        void SetAttribute(string attributeName, string value);
    }
}