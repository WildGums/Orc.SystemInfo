namespace Orc.SystemInfo;

using System;
using System.Collections.Generic;
using System.Linq;
using Catel.Logging;
using Catel.Services;
using Microsoft.Extensions.Logging;
using Wmi;

public class WmiOperatingSystemSystemInfoProvider : ISystemInfoProvider
{
    private readonly ILogger<WmiOperatingSystemSystemInfoProvider> _logger;
    private readonly ILanguageService _languageService;

    public WmiOperatingSystemSystemInfoProvider(ILogger<WmiOperatingSystemSystemInfoProvider> logger, ILanguageService languageService)
    {
        _logger = logger;
        _languageService = languageService;
    }

    public IEnumerable<SystemInfoElement> GetSystemInfoElements()
    {
        var notAvailable = _languageService.GetRequiredString("SystemInfo_NotAvailable");

        var items = new List<SystemInfoElement>();

        try
        {
            WindowsManagementObject? wmi;

            const string wql = "SELECT * FROM Win32_OperatingSystem";

            using (var connection = new WindowsManagementConnection(_logger))
            {
                wmi = connection.CreateQuery(wql).FirstOrDefault();
            }

            if (wmi is null)
            {
                throw _logger.LogErrorAndCreateException<InvalidOperationException>($"Unexpected result from query: {wql}");
            }

            items.Add(new SystemInfoElement(_languageService.GetRequiredString("SystemInfo_OsName"), wmi.GetRequiredValue("Caption", notAvailable)));
            // Note: can be retrieved from SystemInfo.wProcessorArchitecture
            items.Add(new SystemInfoElement(_languageService.GetRequiredString("SystemInfo_Architecture"), wmi.GetRequiredValue("OSArchitecture", notAvailable)));
            items.Add(new SystemInfoElement(_languageService.GetRequiredString("SystemInfo_Build"), wmi.GetRequiredValue("BuildNumber", notAvailable)));
            // Note: can be count from lpMaximumApplicationAddress (Kernel32.SystemInfo);
            items.Add(new SystemInfoElement(_languageService.GetRequiredString("SystemInfo_MaxProcessRam"), (wmi.GetValue("MaxProcessMemorySize", Convert.ToInt64)).ToReadableSize(1))); // KB
        }
        catch (Exception ex)
        {
            items.Add(new SystemInfoElement(_languageService.GetRequiredString("SystemInfo_OsInfo"), "n/a, please contact support"));
            _logger.LogWarning(ex, "Failed to retrieve OS information");
        }

        return items;
    }
}
