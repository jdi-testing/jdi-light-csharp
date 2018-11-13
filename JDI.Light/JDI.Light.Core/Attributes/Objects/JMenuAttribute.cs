using System;

namespace JDI.Web.Attributes.Objects
{
    [AttributeUsage(AttributeTargets.All, Inherited = false)]
    public class JMenuAttribute : Attribute
    {
        public FindByAttribute[] LevelLocators = null;
        public string Separator = "";
    }
}