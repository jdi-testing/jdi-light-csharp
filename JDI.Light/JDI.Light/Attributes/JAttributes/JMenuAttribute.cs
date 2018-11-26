using System;

namespace JDI.Core.Attributes.JAttributes
{
    [AttributeUsage(AttributeTargets.All, Inherited = false)]
    public class JMenuAttribute : Attribute
    {
        public FindByAttribute[] LevelLocators = null;
        public string Separator = "";
    }
}