namespace DellFanManagement.SmmIo
{
    public enum ThermalSetting : uint
    {
        Error = 0,
        Optimized = 1,
        Cool = 2,
        Quiet = 4,
        Performance = 8
    }
}
