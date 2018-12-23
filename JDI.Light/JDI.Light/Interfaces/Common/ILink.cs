namespace JDI.Light.Interfaces.Common
{
    public interface ILink : IText
    {
        string GetReference();
        string WaitReferenceContains(string text);
        string WaitMatchReference(string regEx);
        string GetTooltip();
    }
}