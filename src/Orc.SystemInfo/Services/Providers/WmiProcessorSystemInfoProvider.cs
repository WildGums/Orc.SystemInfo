namespace Orc.SystemInfo;

using System;
using System.Collections.Generic;
using System.Linq;
using Catel.Logging;
using Catel.Services;
using Microsoft.Extensions.Logging;
using Wmi;

public class WmiProcessorSystemInfoProvider : ISystemInfoProvider
{
    private readonly ILogger<WmiProcessorSystemInfoProvider> _logger;
    private readonly ILanguageService _languageService;

    public WmiProcessorSystemInfoProvider(ILogger<WmiProcessorSystemInfoProvider> logger, ILanguageService languageService)
    {
        _logger = logger;
        _languageService = languageService;
    }

    public IEnumerable<SystemInfoElement> GetSystemInfoElements()
    {
        _logger.LogDebug("Retrieving system info");

        var notAvailable = _languageService.GetRequiredString("SystemInfo_NotAvailable");

        var items = new List<SystemInfoElement>();

        try
        {
            WindowsManagementObject? cpu = null;
            const string wql = "SELECT * FROM Win32_Processor";

            using (var connection = new WindowsManagementConnection(_logger))
            {
                cpu = connection.CreateQuery(wql).FirstOrDefault();
            }

            if (cpu is null)
            {
                throw _logger.LogErrorAndCreateException<InvalidOperationException>($"Unexpected result from query: {wql}");
            }

            // __cpuid, see: https://docs.microsoft.com/ru-ru/cpp/intrinsics/cpuid-cpuidex?view=msvc-160;
            items.Add(new SystemInfoElement(_languageService.GetRequiredString("SystemInfo_ProcessorId"), cpu.GetRequiredValue("ProcessorId", notAvailable)));
            items.Add(new SystemInfoElement(_languageService.GetRequiredString("SystemInfo_CpuName"), cpu.GetRequiredValue("Name", notAvailable)));
            items.Add(new SystemInfoElement(_languageService.GetRequiredString("SystemInfo_Description"), cpu.GetRequiredValue("Caption", notAvailable)));
            items.Add(new SystemInfoElement(_languageService.GetRequiredString("SystemInfo_AddressWidth"), cpu.GetRequiredValue<int>("AddressWidth", notAvailable)));
            items.Add(new SystemInfoElement(_languageService.GetRequiredString("SystemInfo_DataWidth"), cpu.GetRequiredValue<int>("DataWidth", notAvailable)));
            items.Add(new SystemInfoElement(_languageService.GetRequiredString("SystemInfo_ClockSpeedMhz"), cpu.GetRequiredValue<int>("MaxClockSpeed", notAvailable)));
            items.Add(new SystemInfoElement(_languageService.GetRequiredString("SystemInfo_BusSpeedMhz"), cpu.GetRequiredValue<int>("ExtClock", notAvailable)));
            items.Add(new SystemInfoElement(_languageService.GetRequiredString("SystemInfo_NumberOfCores"), cpu.GetRequiredValue<int>("NumberOfCores", notAvailable)));
            items.Add(new SystemInfoElement(_languageService.GetRequiredString("SystemInfo_NumberOfLogicalProcessors"), cpu.GetRequiredValue<int>("NumberOfLogicalProcessors", notAvailable)));
        }
        catch (Exception ex)
        {
            items.Add(new SystemInfoElement(_languageService.GetRequiredString("SystemInfo_CpuInfo"), "n/a, please contact support"));
            _logger.LogWarning(ex, "Failed to retrieve CPU information");
        }

        return items;
    }
}
