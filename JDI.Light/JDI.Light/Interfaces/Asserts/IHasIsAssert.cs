using JDI.Light.Asserts;

namespace JDI.Light.Interfaces.Asserts
{
    public interface IHasIsAssert
    {
        IsAssert Is { get; }
        IsAssert AssertThat { get; }
        IsAssert Has { get; }
        IsAssert WaitFor { get; }
        IsAssert ShouldBe { get; }
    }
}
