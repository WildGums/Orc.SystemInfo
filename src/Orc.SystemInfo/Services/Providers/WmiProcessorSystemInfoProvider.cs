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
            _languageService = languageService;
        }

        public IEnumerable<SystemInfoElement> GetSystemInfoElements()
        {
            Log.Debug("Retrieving system info");

            var notAvailable = _languageService.GetString("SystemInfo_NotAvailable");

            var items = new List<SystemInfoElement>();

            try
            {
                WindowsManagementObject cpu = null;
                var wql = "SELECT * FROM Win32_Processor";

                using (WindowsManagementConnection connection = new WindowsManagementConnection())
                {
                    cpu = connection.CreateQuery(wql).FirstOrDefault();
                }

                if (cpu is null)
                {
                    throw Log.ErrorAndCreateException<InvalidOperationException>($"Unexpected result from query: {wql}");
                }

                // __cpuid, see: https://docs.microsoft.com/ru-ru/cpp/intrinsics/cpuid-cpuidex?view=msvc-160;
                items.Add(new SystemInfoElement(_languageService.GetString("SystemInfo_ProcessorId"), cpu.GetValue("ProcessorId", notAvailable)));
                items.Add(new SystemInfoElement(_languageService.GetString("SystemInfo_CpuName"), cpu.GetValue("Name", notAvailable)));
                items.Add(new SystemInfoElement(_languageService.GetString("SystemInfo_Description"), cpu.GetValue("Caption", notAvailable)));
                items.Add(new SystemInfoElement(_languageService.GetString("SystemInfo_AddressWidth"), cpu.GetValue<int>("AddressWidth", notAvailable)));
                items.Add(new SystemInfoElement(_languageService.GetString("SystemInfo_DataWidth"), cpu.GetValue<int>("DataWidth", notAvailable)));
                items.Add(new SystemInfoElement(_languageService.GetString("SystemInfo_ClockSpeedMHz"), cpu.GetValue<int>("MaxClockSpeed", notAvailable)));
                items.Add(new SystemInfoElement(_languageService.GetString("SystemInfo_BusSpeedMHz"), cpu.GetValue<int>("ExtClock", notAvailable)));
                items.Add(new SystemInfoElement(_languageService.GetString("SystemInfo_NumberOfCores"), cpu.GetValue<int>("NumberOfCores", notAvailable)));
                items.Add(new SystemInfoElement(_languageService.GetString("SystemInfo_NumberOfLogicalProcessors"), cpu.GetValue<int>("NumberOfLogicalProcessors", notAvailable)));
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
