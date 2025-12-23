namespace Orc.SystemInfo;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using Catel.Collections;
using Catel.Services;
using MethodTimer;
using Microsoft.Extensions.Logging;
using Win32;

public class SystemInfoService : ISystemInfoService
{
    private readonly ILogger<SystemInfoService> _logger;
    private readonly IDotNetFrameworkService _dotNetFrameworkService;
    private readonly ILanguageService _languageService;
    private readonly IDbProvidersService _dbProviderService;
    private readonly IReadOnlyList<ISystemInfoProvider> _systemInfoProviders;

    public SystemInfoService(ILogger<SystemInfoService> logger, IDotNetFrameworkService dotNetFrameworkService, ILanguageService languageService,
        IDbProvidersService dbProviderService, IEnumerable<ISystemInfoProvider> systemInfoProviders)
    {
        _logger = logger;
        _dotNetFrameworkService = dotNetFrameworkService;
        _languageService = languageService;
        _dbProviderService = dbProviderService;
        _systemInfoProviders = systemInfoProviders.ToArray();
    }

    [Time]
    public IEnumerable<SystemInfoElement> GetSystemInfo()
    {
        _logger.LogDebug("Retrieving system info");

        var items = new List<SystemInfoElement>
        {
            new (_languageService.GetRequiredString("SystemInfo_UserName"), Environment.UserName),
            new (_languageService.GetRequiredString("SystemInfo_UserDomainName"), Environment.UserDomainName),
            new (_languageService.GetRequiredString("SystemInfo_MachineName"), Environment.MachineName),
            new (_languageService.GetRequiredString("SystemInfo_OsVersion"), Environment.OSVersion.ToString()),
            new (_languageService.GetRequiredString("SystemInfo_Version"), Environment.Version.ToString())
        };

        foreach (var systemInfoProvider in _systemInfoProviders)
        {
            items.AddRange(systemInfoProvider.GetSystemInfoElements());
        }

        var memStatus = new Kernel32.MemoryStatusEx();
        if (Kernel32.GlobalMemoryStatusEx(memStatus))
        {
            items.Add(new SystemInfoElement(_languageService.GetRequiredString("SystemInfo_TotalMemory"), memStatus.ullTotalPhys.ToReadableSize()));
            items.Add(new SystemInfoElement(_languageService.GetRequiredString("SystemInfo_AvailableMemory"), memStatus.ullAvailPhys.ToReadableSize()));
        }

        items.Add(new SystemInfoElement(_languageService.GetRequiredString("SystemInfo_SystemUpTime"), GetSystemUpTime()));
        items.Add(new SystemInfoElement(_languageService.GetRequiredString("SystemInfo_ApplicationUpTime"), (DateTime.Now - Process.GetCurrentProcess().StartTime).ToString()));

        items.Add(new SystemInfoElement(_languageService.GetRequiredString("SystemInfo_CurrentCulture"), CultureInfo.CurrentCulture.ToString()));

        items.Add(new SystemInfoElement(_languageService.GetRequiredString("SystemInfo_DotNetFrameworkVersions"), string.Empty));

        foreach (var pair in _dotNetFrameworkService.GetInstalledFrameworks())
        {
            items.Add(new SystemInfoElement(string.Empty, pair));
        }

        items.Add(new SystemInfoElement(_languageService.GetRequiredString("SystemInfo_InstalledDatabaseProviders"), string.Empty));

        foreach (var dbProviderName in _dbProviderService.GetInstalledDbProviders())
        {
            items.Add(new SystemInfoElement(string.Empty, dbProviderName));
        }

        _logger.LogDebug("Retrieved system info");

        return items;
    }

    private static string GetSystemUpTime()
    {
        try
        {
            using var upTime = new PerformanceCounter("System", "System Up Time");
            upTime.NextValue();
            return TimeSpan.FromSeconds(upTime.NextValue()).ToString();
        }
        catch (Exception)
        {
            return "n/a";
        }
    }
}
