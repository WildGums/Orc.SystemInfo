namespace Orc.SystemInfo
{
    using System.Collections.Generic;

    public interface IDotNetFrameworkService
    {
        IEnumerable<string> GetInstalledFrameworks();
    }
}
