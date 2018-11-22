using System;

namespace JDI.Core.Attributes.JAttributes
{
    [AttributeUsage(AttributeTargets.All, Inherited = false)]
    public class JDropdownAttribute : Attribute
    {
        public FindByAttribute ElementByName = null;
        public FindByAttribute Expand = null;
        public FindByAttribute List = null;
        public FindByAttribute Root = null;
        public FindByAttribute Value = null;
    }
}