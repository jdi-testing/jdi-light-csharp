namespace JDI.Light.Interfaces.Common
{
    public interface ITextArea : ITextField
    {
        void InputLines(params string[] textLines);
        void AddNewLine(string textLine);
        string[] GetLines();
    }
}