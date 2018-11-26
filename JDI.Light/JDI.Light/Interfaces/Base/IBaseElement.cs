namespace JDI.Light.Interfaces.Base
{
    public interface IBaseElement
    {
        string DriverName { get; set; }
        string Name { get; set; }
        IBaseElement Parent { set; get; }
    }
}