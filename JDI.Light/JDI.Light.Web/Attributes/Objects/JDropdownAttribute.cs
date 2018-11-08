using System;

namespace JDI.Web.Attributes.Objects
{
    [AttributeUsage(AttributeTargets.All, Inherited = false)]
    public class JDropdownAttribute : Attribute
    {
        public FindByAttribute Root = null;
        public FindByAttribute Value = null;
        public FindByAttribute List = null;
        public FindByAttribute Expand = null;
        public FindByAttribute ElementByName = null;

    }
}
