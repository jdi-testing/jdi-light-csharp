using JDI.Light.Interfaces.Common;

namespace JDI.Light.Interfaces.Composite
{
    public interface ISearch : ITextField
    {
        void Find(string text);
    }
}