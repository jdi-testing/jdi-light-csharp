using JDI.Light.Asserts.Generic;
using JDI.Light.Interfaces.Complex;
using static JDI.Light.Jdi;

namespace JDI.Light.Asserts
{
    public class MenuSelectAssert : IsAssert<SelectAssert>
    {
        private readonly IMenuSelector _selector;

        public MenuSelectAssert(IMenuSelector selector) : base(selector)
        {
            _selector = selector;
        }

        public MenuSelectAssert Selected(string option)
        {
            Assert.IsTrue(_selector.Selected(option), $"no {option} in {string.Join(",", _selector.Selected(option))}");
            return this;
        }
    }
}
