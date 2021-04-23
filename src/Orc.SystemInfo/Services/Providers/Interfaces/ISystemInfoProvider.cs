namespace Orc.SystemInfo
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface ISystemInfoProvider
    {
        IEnumerable<SystemInfoElement> GetSystemInfoElements();
    }
}
