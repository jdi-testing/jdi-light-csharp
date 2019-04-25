using JDI.Light.Asserts;

namespace JDI.Light.Interfaces.Asserts
{
    public interface IHasSelectAssert
    {
        SelectAssert Is { get; }
        SelectAssert AssertThat { get; }
        SelectAssert Has { get; }
    }
}
