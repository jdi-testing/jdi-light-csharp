namespace JDI.Light.Interfaces.Common
{
    public interface ITextArea : ITextField
    {
        void SetLines(params string[] textLines);
        string[] GetLines();
        int Rows();
        int Cols();
        int MinLength();
        int MaxLength();
        void AddNewLine(string textLine);
    }
}