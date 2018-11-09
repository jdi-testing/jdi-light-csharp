namespace JDI.Core.Interfaces.Base
{
    public interface IHasParent
    {
        void SetParent(object parent);
        object GetParent();
    }
}