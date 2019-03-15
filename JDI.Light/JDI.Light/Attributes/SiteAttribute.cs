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

        public Func<string> GetDomainFunc
        {
            get
            {
                var d = (Func<string>)Delegate.CreateDelegate(typeof(Func<string>), DomainProviderType,
                    DomainProviderMethodName, true, false);
                if (d == null)
                {
                    throw new MissingMethodException(
                        "Can't get the method returning site domain. " +
                               $"Please make sure your method '{DomainProviderMethodName}' " +
                        $"from '{DomainProviderType}' is public static Func<string>.");
                }
                return d;
            }
        }

        public string Domain { get; set; }
        public bool UseCache { get; set; }
        public Type DomainProviderType { get; set; }
        public string DomainProviderMethodName { get; set; }
    }
}