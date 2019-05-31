using JDI.Light.Elements.Common;

namespace JDI.Light.Interfaces.Base
{
    public interface IHasLabel
    {
        Label Label(string forId = "");
        string LabelText(string forId = "");
    }
}
