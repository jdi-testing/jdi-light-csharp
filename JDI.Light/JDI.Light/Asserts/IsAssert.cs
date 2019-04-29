using JDI.Light.Asserts.Generic;
using JDI.Light.Interfaces.Base;

namespace JDI.Light.Asserts
{
    public class IsAssert : IsAssert<IsAssert>
    {
        public IsAssert(IBaseUIElement element) : base(element)
        {
        }
    }
}