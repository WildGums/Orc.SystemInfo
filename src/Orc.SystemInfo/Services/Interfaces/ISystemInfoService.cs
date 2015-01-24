namespace Orc.SystemInfo.Services
{
    using System.Collections.Generic;
    using Models;

    public interface ISystemInfoService
    {
        IEnumerable<Pair<string, string>> GetSystemInfo();
    }
}