namespace JDI.Light.Interfaces.Common
{
    public interface ILink : ITextElement
    {
        string GetReference();
        string WaitReferenceContains(string text);
        string WaitReferenceMatches(string regEx);
        string GetTooltip();
    }
}