namespace Orc.SystemInfo
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Management;
    using Catel.Logging;
    using Catel.Services;

    public class WmiOperatingSystemSystemInfoProvider : ISystemInfoProvider
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        private readonly ILanguageService _languageService;

        public WmiOperatingSystemSystemInfoProvider(ILanguageService languageService)
        {
            _languageService = languageService;
        }

        public IEnumerable<SystemInfoElement> GetSystemInfoElements()
        {
            var notAvailable = _languageService.GetString("SystemInfo_NotAvailable");
            var items = new List<SystemInfoElement>();

            try
            {
                var wmi = new ManagementObjectSearcher("select * from Win32_OperatingSystem")
                    .Get()
                    .Cast<ManagementObject>()
                    .First();

                items.Add(new SystemInfoElement(_languageService.GetString("SystemInfo_OsName"), wmi.GetValue("Caption", notAvailable)));
                items.Add(new SystemInfoElement(_languageService.GetString("SystemInfo_Architecture"), wmi.GetValue("OSArchitecture", notAvailable)));
                // __cpuid, see: https://docs.microsoft.com/ru-ru/cpp/intrinsics/cpuid-cpuidex?view=msvc-160;
                items.Add(new SystemInfoElement(_languageService.GetString("SystemInfo_ProcessorId"), wmi.GetValue("ProcessorId", notAvailable)));
                items.Add(new SystemInfoElement(_languageService.GetString("SystemInfo_Build"), wmi.GetValue("BuildNumber", notAvailable)));
                // count from lpMaximumApplicationAddress;
                items.Add(new SystemInfoElement(_languageService.GetString("SystemInfo_MaxProcossRam"), (wmi.GetLongValue("MaxProcessMemorySize")).ToReadableSize(1))); // KB
            }
            catch (Exception ex)
            {
                items.Add(new SystemInfoElement(_languageService.GetString("SystemInfo_OsInfo"), "n/a, please contact support"));
                Log.Warning(ex, "Failed to retrieve OS information");
            }

            return items;
        }
    }
}
