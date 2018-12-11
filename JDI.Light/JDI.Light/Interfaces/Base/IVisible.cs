namespace JDI.Light.Interfaces.Base
{
    public interface IVisible
    {
        bool Displayed { get; }
        bool Hidden { get; }
        void WaitDisplayed();
        void WaitVanished();
    }
}