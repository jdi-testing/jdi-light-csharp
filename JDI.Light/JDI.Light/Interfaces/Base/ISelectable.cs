namespace JDI.Light.Interfaces.Base
{
    public interface ISelectable<T> : ISetValue<T>, IClickable
    {
        bool Selected { get; }
        void Select();
    }
}