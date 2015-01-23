namespace Orc.SystemInfo.Services
{
    using System.Collections.Generic;

    public interface ISystemInfoService
    {
        IEnumerable<KeyValuePair<string, string>> GetSystemInfo();
    }
}