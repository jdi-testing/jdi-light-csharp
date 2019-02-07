namespace JDI.Light.Interfaces.Utils
{
    public interface IKillDriver
    {
        string[] ProcessToKill { get; set; }
        void KillAllRunningDrivers();
    }
}