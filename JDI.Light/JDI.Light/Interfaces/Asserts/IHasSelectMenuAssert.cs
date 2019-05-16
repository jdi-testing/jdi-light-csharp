using JDI.Light.Asserts;

namespace JDI.Light.Interfaces.Asserts
{
    public interface IHasSelectMenuAssert
    {
        MenuSelectAssert Is { get; }
        MenuSelectAssert AssertThat { get; }
    }
}
