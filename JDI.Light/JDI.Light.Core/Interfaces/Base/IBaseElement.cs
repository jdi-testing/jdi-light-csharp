using System.Reflection;
using JDI.Core.Attributes.Functions;

namespace JDI.Core.Interfaces.Base
{
    public interface IBaseElement
    {
        string Name { get; set; }
        string TypeName { get; set; }
        IAvatar Avatar { get; set; }
        object Parent { set; get; }
        void SetName(FieldInfo field);
        void SetFunction(Functions function);
    }
}