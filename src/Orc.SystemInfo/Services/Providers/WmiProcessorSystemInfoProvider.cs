namespace Orc.SystemInfo
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Catel.Logging;
    using Catel.Services;
    using Orc.SystemInfo.Wmi;

    public class WmiProcessorSystemInfoProvider : ISystemInfoProvider
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        private readonly ILanguageService _languageService;

        public WmiProcessorSystemInfoProvider(ILanguageService languageService)
        {
            ArgumentNullException.ThrowIfNull(languageService);

            _languageService = languageService;
        }

        public IEnumerable<SystemInfoElement> GetSystemInfoElements()
        {
            Log.Debug("Retrieving system info");

            var notAvailable = _languageService.GetRequiredString("SystemInfo_NotAvailable");

            var items = new List<SystemInfoElement>();

            try
            {
                WindowsManagementObject? cpu = null;
                var wql = "SELECT * FROM Win32_Processor";

                using (var connection = new WindowsManagementConnection())
                {
                    cpu = connection.CreateQuery(wql).FirstOrDefault();
                }

                if (cpu is null)
                {
                    throw Log.ErrorAndCreateException<InvalidOperationException>($"Unexpected result from query: {wql}");
                }

                // __cpuid, see: https://docs.microsoft.com/ru-ru/cpp/intrinsics/cpuid-cpuidex?view=msvc-160;
                items.Add(new SystemInfoElement(_languageService.GetRequiredString("SystemInfo_ProcessorId"), cpu.GetRequiredValue("ProcessorId", notAvailable)));
                items.Add(new SystemInfoElement(_languageService.GetRequiredString("SystemInfo_CpuName"), cpu.GetRequiredValue("Name", notAvailable)));
                items.Add(new SystemInfoElement(_languageService.GetRequiredString("SystemInfo_Description"), cpu.GetRequiredValue("Caption", notAvailable)));
                items.Add(new SystemInfoElement(_languageService.GetRequiredString("SystemInfo_AddressWidth"), cpu.GetRequiredValue<int>("AddressWidth", notAvailable)));
                items.Add(new SystemInfoElement(_languageService.GetRequiredString("SystemInfo_DataWidth"), cpu.GetRequiredValue<int>("DataWidth", notAvailable)));
                items.Add(new SystemInfoElement(_languageService.GetRequiredString("SystemInfo_ClockSpeedMHz"), cpu.GetRequiredValue<int>("MaxClockSpeed", notAvailable)));
                items.Add(new SystemInfoElement(_languageService.GetRequiredString("SystemInfo_BusSpeedMHz"), cpu.GetRequiredValue<int>("ExtClock", notAvailable)));
                items.Add(new SystemInfoElement(_languageService.GetRequiredString("SystemInfo_NumberOfCores"), cpu.GetRequiredValue<int>("NumberOfCores", notAvailable)));
                items.Add(new SystemInfoElement(_languageService.GetRequiredString("SystemInfo_NumberOfLogicalProcessors"), cpu.GetRequiredValue<int>("NumberOfLogicalProcessors", notAvailable)));
            }
            catch (Exception ex)
            {
                items.Add(new SystemInfoElement(_languageService.GetRequiredString("SystemInfo_CpuInfo"), "n/a, please contact support"));
                Log.Warning(ex, "Failed to retrieve CPU information");
            }

            return items;
        }
    }
}
