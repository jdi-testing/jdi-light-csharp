using JDI.Light.Asserts;

namespace JDI.Light.Interfaces.Common
{
    public interface ITextArea : ITextField
    {
        void SetLines(bool checkEnabled = true, params string[] textLines);
        string[] GetLines();
        int Rows();
        int Cols();
        int MinLength();
        int MaxLength();
        void AddNewLine(string textLine, bool checkEnabled = true);

        new TextAreaAssert Is();
        new TextAreaAssert AssertThat();
    }
}