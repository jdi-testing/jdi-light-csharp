namespace JDI.Light.Interfaces.Base
{
    public interface IBaseElement
    {
        ILogger Logger { get; }
        string DriverName { get; set; }
        string Name { get; set; }
        IBaseElement Parent { set; get; }

        void SetUp(ILogger logger);
    }
}