using System;

namespace JDI.Light.Attributes.JAttributes
{
    [AttributeUsage(AttributeTargets.All, Inherited = false)]
    public class JMenuAttribute : Attribute
    {
        public FindByAttribute[] LevelLocators = null;
        public string Separator = "";
    }
}