using System;

namespace JDI.Light.Attributes
{
    [AttributeUsage(AttributeTargets.All, Inherited = false)]
    public class NameAttribute : Attribute
    {
        public readonly string Name;

        public NameAttribute(string name)
        {
            Name = name;
        }
    }
}