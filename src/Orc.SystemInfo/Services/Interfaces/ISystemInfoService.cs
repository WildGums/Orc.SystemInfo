namespace Orc.SystemInfo
{
    using System.Collections.Generic;

    public interface ISystemInfoService
    {
        IEnumerable<SystemInfoElement> GetSystemInfo();
    }
}
