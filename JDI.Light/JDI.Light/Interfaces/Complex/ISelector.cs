using System.Collections.Generic;
using JDI.Light.Interfaces.Asserts;
using JDI.Light.Interfaces.Base;

namespace JDI.Light.Interfaces.Complex
{
    public interface ISelector : IBaseUIElement, IHasSize, IHasSelectAssert
    {
        bool Selected(string option);
        List<string> Checked();
        List<string> Values();
        List<string> ListEnabled();
        List<string> ListDisabled();
        List<IBaseUIElement> AllUI();
    }
}