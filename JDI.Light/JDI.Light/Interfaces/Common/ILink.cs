using JDI.Light.Asserts;

namespace JDI.Light.Interfaces.Common
{
    public interface ILink : ITextElement
    {
        string Ref();
        string Url();
        string Alt();

        new LinkAssert Is();
        new LinkAssert AssertThat();
    }
}