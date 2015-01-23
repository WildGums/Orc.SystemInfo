namespace Orc.SystemInfo.Services
{
    using System.Collections.Generic;
    using Models;

    public interface ISystemInfoService
    {
        IEnumerable<CoupledValue<string, string>> GetSystemInfo();
    }
}