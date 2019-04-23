namespace JDI.Light.Interfaces.Complex
{
    public interface IHasSize
    {
        int Size { get; }
        bool IsEmpty();
        bool HasAny();
    }
}
