﻿using System.Collections.Generic;
using JDI.Light.Asserts;
using JDI.Light.Interfaces.Base;

namespace JDI.Light.Interfaces.Complex
{
    public interface ISelector : IHasSize
    {
        bool Selected(string option);
        List<string> Checked();
        List<string> Values();
        List<string> ListEnabled();
        List<string> ListDisabled();
        List<IBaseUIElement> AllUI();
        SelectAssert Is();
        SelectAssert AssertThat();
    }
}