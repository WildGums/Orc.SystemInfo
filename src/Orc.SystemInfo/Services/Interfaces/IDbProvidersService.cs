namespace Orc.SystemInfo;

using System.Collections.Generic;

public interface IDbProvidersService
{
    IEnumerable<string> GetInstalledDbProviders();
}