namespace JDI.Light.Interfaces.Base
{
    public interface ISetValue<T> : IGetValue<T>
    {
        new T Value { get; set; }
    }
}