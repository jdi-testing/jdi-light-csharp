namespace JDI.Core.Interfaces.Base
{
    public interface IBaseElement
    {
        string DriverName { get; set; }
        string Name { get; set; }
        object Parent { set; get; }
    }
}