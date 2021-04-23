namespace Orc.SystemInfo
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Management;
    using Catel.Logging;
    using Catel.Services;
    public class WmiProcessorSystemInfoProvider : ISystemInfoProvider
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        private readonly ILanguageService _languageService;

        public WmiProcessorSystemInfoProvider(ILanguageService languageService)
        {
            _languageService = languageService;
        }

        public IEnumerable<SystemInfoElement> GetSystemInfoElements()
        {
            Log.Debug("Retrieving system info");

            var notAvailable = _languageService.GetString("SystemInfo_NotAvailable");

            var items = new List<SystemInfoElement>();

            try
            {
                var cpu = new ManagementObjectSearcher("select * from Win32_Processor")
                    .Get()
                    .Cast<ManagementObject>()
                    .First();

                items.Add(new SystemInfoElement(_languageService.GetString("SystemInfo_CpuName"), cpu.GetValue("Name", notAvailable)));
                items.Add(new SystemInfoElement(_languageService.GetString("SystemInfo_Description"), cpu.GetValue("Caption", notAvailable)));
                items.Add(new SystemInfoElement(_languageService.GetString("SystemInfo_AddressWidth"), cpu.GetValue("AddressWidth", notAvailable)));
                items.Add(new SystemInfoElement(_languageService.GetString("SystemInfo_DataWidth"), cpu.GetValue("DataWidth", notAvailable)));
                items.Add(new SystemInfoElement(_languageService.GetString("SystemInfo_ClockSpeedMHz"), cpu.GetValue("MaxClockSpeed", notAvailable)));
                items.Add(new SystemInfoElement(_languageService.GetString("SystemInfo_BusSpeedMHz"), cpu.GetValue("ExtClock", notAvailable)));
                items.Add(new SystemInfoElement(_languageService.GetString("SystemInfo_NumberOfCores"), cpu.GetValue("NumberOfCores", notAvailable)));
                // GetNativeSystemInfo
                items.Add(new SystemInfoElement(_languageService.GetString("SystemInfo_NumberOfLogicalProcessors"), cpu.GetValue("NumberOfLogicalProcessors", notAvailable)));
            }
            catch (Exception ex)
            {
                items.Add(new SystemInfoElement(_languageService.GetString("SystemInfo_CpuInfo"), "n/a, please contact support"));
                Log.Warning(ex, "Failed to retrieve CPU information");
            }

            return items;
        }
    }
}
