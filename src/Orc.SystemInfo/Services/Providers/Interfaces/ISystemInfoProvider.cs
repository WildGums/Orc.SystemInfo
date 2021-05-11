namespace Orc.SystemInfo
{
    using System.Collections.Generic;

    public interface ISystemInfoProvider
    {
        IEnumerable<SystemInfoElement> GetSystemInfoElements();
    }
}
