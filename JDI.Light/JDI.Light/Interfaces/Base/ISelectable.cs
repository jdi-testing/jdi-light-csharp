namespace JDI.Light.Interfaces.Base
{
    public interface ISelectable<T> : ISetValue<T>
    {
        bool Selected { get; }
        void Select();
    }
}