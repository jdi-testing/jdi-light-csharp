using System.Reflection;
using JDI.Core.Attributes.Functions;

namespace JDI.Core.Interfaces.Base
{
    public interface IBaseElement
    {
        string DriverName { get; set; }
        string Name { get; set; }
        string TypeName { get; set; }
        object Parent { set; get; }
        void SetName(FieldInfo field);
        void SetFunction(Functions function);
        void WaitAttribute(string name, string value);
        string GetAttribute(string name);
        void SetAttribute(string attributeName, string value);
    }
}