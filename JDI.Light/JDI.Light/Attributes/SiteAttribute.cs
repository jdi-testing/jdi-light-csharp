using System;

namespace JDI.Light.Attributes
{
    [AttributeUsage(AttributeTargets.All, Inherited = false)]
    public class SiteAttribute : Attribute
    {
        public SiteAttribute()
        {
            IsMain = true;
            UseCache = true;
        }

        public string Domain { get; set; }
        public bool UseCache { get; set; }
        public bool IsMain { get; set; }
    }
}