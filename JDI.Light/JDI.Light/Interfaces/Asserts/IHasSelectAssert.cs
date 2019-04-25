using JDI.Light.Asserts;

namespace JDI.Light.Interfaces.Asserts
{
    public interface IHasSelectAssert
    {
        SelectAssert Is();
        SelectAssert AssertThat();
        SelectAssert Has();
    }
}
