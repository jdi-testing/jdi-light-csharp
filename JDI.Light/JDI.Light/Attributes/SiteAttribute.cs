using System;

namespace JDI.Light.Attributes
{
    [AttributeUsage(AttributeTargets.All, Inherited = false)]
    public class SiteAttribute : Attribute
    {
        public SiteAttribute()
        {
            UseCache = true;
        }

        public Func<string> GetDomainFunc => (Func<string>)Delegate.CreateDelegate(typeof(Func<string>), DomainProviderType, DomainProviderMethodName, true, false);
        public string Domain { get; set; }
        public bool UseCache { get; set; }
        public Type DomainProviderType { get; set; }
        public string DomainProviderMethodName { get; set; }
    }
}