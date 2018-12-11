namespace JDI.Light.Interfaces.Base
{
    public interface IGetValue<T>
    {
        T Value { get; }
        T GetValue();
    }
}