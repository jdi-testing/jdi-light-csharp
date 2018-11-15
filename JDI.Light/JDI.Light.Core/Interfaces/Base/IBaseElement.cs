using System.Reflection;
using JDI.Core.Attributes.Functions;
using JDI.Core.Selenium.Elements.APIInteract;

namespace JDI.Core.Interfaces.Base
{
    public interface IBaseElement
    {
        string Name { get; set; }
        string TypeName { get; set; }
        object Parent { set; get; }
        WebAvatar WebAvatar { get; set; }
        void SetName(FieldInfo field);
        void SetFunction(Functions function);
    }
}