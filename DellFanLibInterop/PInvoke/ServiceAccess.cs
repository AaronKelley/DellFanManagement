namespace DellFanManagement.Interop.PInvoke
{
    /// <summary>
    /// Specific access rights for the Service Control Manager.
    /// </summary>
    /// <seealso cref="http://www.pinvoke.net/default.aspx/Enums/SERVICE_ACCESS.html"/>
    [System.Flags]
    public enum ServiceAccess : uint
    {
        StandardRightsRequred = 0xF0000,
        QueryConfig = 0x00001,
        ChangeConfig = 0x00002,
        QueryStatus = 0x00004,
        EnumerateDependents = 0x00008,
        Start = 0x00010,
        Stop = 0x00020,
        PauseContinue = 0x00040,
        Interrogate = 0x00080,
        UserDefinedControl = 0x00100,
        AllAccess = (StandardRightsRequred |
            QueryConfig |
            ChangeConfig |
            QueryStatus |
            EnumerateDependents |
            Start |
            Stop |
            PauseContinue |
            Interrogate |
            UserDefinedControl)
    }
}
