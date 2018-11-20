using JDI.Core.Attributes.Functions;

namespace JDI.Core.Interfaces.Base
{
    public interface IBaseElement
    {
        string DriverName { get; set; }
        string Name { get; set; }
        string TypeName { get; set; }
        object Parent { set; get; }
        void SetFunction(Functions function);
        string GetAttribute(string name);
        void SetAttribute(string attributeName, string value);
    }
}