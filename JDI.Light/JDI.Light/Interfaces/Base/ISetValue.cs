namespace JDI.Core.Interfaces.Base
{
    public interface ISetValue : IHasValue
    {
        new string Value { get; set; }
    }
}