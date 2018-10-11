namespace JDI.Light.Elements.Interfaces
{
    public interface IBaseElement : IHasValue
    {
        void Hover();
        bool IsEnabled();
        bool IsDisplayed();
        //Point GetLocation();
        //Dimension getSize();
        //Rectangle getRect();
        //<X> X getScreenshotAs(OutputType<X> outputType) throws WebDriverException;
        string GetAttribute(string name);
        void SetAttribute(string name, string value);
        void Highlight(string color);
        void Highlight();
        void Show();
        //Select select();
    }
}